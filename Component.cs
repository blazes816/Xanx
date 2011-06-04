using System;
using System.Collections.Generic;

namespace Xanx
{
    public class Component
    {
        string name;

        public string Name
        {
            get { return name; }
            set 
            {
                // Make sure we have a valid name before allowing it to be
                // changed
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Component name must not be null "
                        + "and be greater than 0 characters");
                else
                    name = value;
            }
        }

        GameScreen parent;

        public GameScreen Parent
        {
            get { return parent; }
            set
            {
                if (parent != null)
                    parent.RemoveComponent(this);

                if (parent != value)
                {
                    parent = value;

                    if (value != null)
                        parent.AddComponent(this);
                }
            }
        }

        int drawOrder = 0;

        public int DrawOrder
        {
            get { return drawOrder; }
            set
            {
                this.drawOrder = value;

                if (Parent != null)
                    Parent.PutComponentInOrder(this);
            }
        }

        bool loaded = false;

        public bool Loaded { get { return loaded; } }

        public Component()
        {
            generateUniqueName();
        }

        public void LoadComponent()
        {
            if (!loaded)
                Load();

            loaded = true;
        }

        protected virtual void Load()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Draw()
        {
        }

        // Keep track of the number of each type of component that have been
        // created, so we can generate a unique name for each component
        static Dictionary<Type, int> componentTypeCounts = new Dictionary<Type, int>();

        // Generate a unique name for the component simply using the type name
        // and the number of that type that have been created
        private void generateUniqueName()
        {
            Type t = this.GetType();

            if (!componentTypeCounts.ContainsKey(t))
                componentTypeCounts.Add(t, 0);

            componentTypeCounts[t]++;

            this.name = t.Name + componentTypeCounts[t];
        }
    }
}
