using System;
using System.Collections.Generic;

using Xanx.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Xanx
{
    public class GameScreen
    {
        public Dictionary<string, Keys[]> Controls = new Dictionary<string, Keys[]>();
        public KeyboardState keyboardState;
        public KeyboardState lastKeyboardState;
        public int windowWidth;
        public int windowHeight;
        public GameTime gameTime;

        List<Component> components = new List<Component>();

        List<Timer> timers = new List<Timer>();

        // Set to internal so Engine can access it without allowing
        // other classes to set engine. Engine must be set through
        // the Engine's PushGameScreen() method so that the stack
        // can be maintained
        internal Engine engine = null;

        public Engine Engine
        {
            get { return engine; }
        }

        bool loaded = false;

        public bool Loaded { get { return loaded; } }

        // Allow for components to be retrieved with the [] index, ie:
        // Component c = gameScreenInstance["Component1"];
        public Component this[string Name]
        {
            get
            {
                foreach (Component component in components)
                    if (component.Name == Name)
                        return component;

                return null;
            }
        }

        public GameScreen(int width, int height)
        {
            // Window size
            this.windowWidth = width;
            this.windowHeight = height;

            this.keyboardState = this.lastKeyboardState = Keyboard.GetState();
            this.gameTime = new GameTime();
        }

        public void LoadGameScreen()
        {
            if (loaded)
                return;

            loaded = true;

            Load();
        }

        protected virtual void Load()
        {
        }

        public void AddComponent(Component Component)
        {
            if (!components.Contains(Component))
            {
                components.Add(Component);
                Component.Parent = this;
                Component.LoadComponent();
                PutComponentInOrder(Component);

                // Store timers so we can delete them when needed
                if (Component.GetType() == typeof(Timer) && !timers.Contains((Timer)Component))
                {
                    timers.Add((Timer)Component);
                }
            }
        }

        // The components are stored in their draw order, so it is easy to loop 
        // through them and draw them in the correct order without having to sort
        // them every time they are drawn
        public void PutComponentInOrder(Component component)
        {
            if (components.Contains(component))
            {
                components.Remove(component);

                int i = 0;

                // Iterate through the components in order until we find one with
                // a higher or equal draw order, and insert the component at that
                // position.
                for (i = 0; i < components.Count; i++)
                    if (components[i].DrawOrder >= component.DrawOrder)
                        break;

                components.Insert(i, component);
            }
        }

        void RemoveComponent(string Name)
        {
            Component c = this[Name];
            RemoveComponent(c);
        }

        public void RemoveComponent(Component Component)
        {
            if (Component != null && components.Contains(Component))
            {
                components.Remove(Component);
                Component.Parent = null;
            }
        }

        public virtual void Update()
        {
            this.gameTime = new GameTime();
            // Copy the list of components so the game won't crash if the original
            // is modified while updating
            List<Component> updating = new List<Component>();

            foreach (Component c in components)
                updating.Add(c);

            foreach (Component c in updating)
                c.Update();

            // Clean up timers
            foreach (Timer t in timers)
                if (t.finished)
                {
                    timers.Remove(t);
                    components.Remove(t);
                    break;
                }
        }

        public virtual void Draw()
        {
            foreach (Component c in components)
                c.Draw();
        }

        public bool isKeySetDown(string keySetKey)
        {
            foreach(Keys key in this.Controls[keySetKey])
                if (this.keyboardState.IsKeyDown(key))
                    return true;

            return false;
        }
    }
}
