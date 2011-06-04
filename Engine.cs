using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/*
 * This game engine is a (to some degree) modified version of the engine from the tutorial at
 * http://www.innovativegames.net/blog/blog/category/tutorialslist/
 */
namespace Xanx
{
    public class Engine
    {
        List<GameScreen> gameScreens = new List<GameScreen>();
        GameTime gameTime = null;
        IEContentManager content = null;
        GraphicsDevice graphicsDevice = null;
        SpriteBatch spriteBatch = null;
        IServiceContainer services = null;

        public GameTime GameTime { get { return gameTime; } }
        public IEContentManager Content { get { return content; } }
        public GraphicsDevice GraphicsDevice { get { return graphicsDevice; } }
        public SpriteBatch SpriteBatch { get { return spriteBatch; } }
        public IServiceContainer Services { get { return services; } }

        public Engine(GraphicsDeviceManager Graphics)
        {
            services = new ServiceContainer();
            services.AddService(typeof(IGraphicsDeviceService), Graphics);
            services.AddService(typeof(IGraphicsDeviceManager), Graphics);

            this.graphicsDevice = Graphics.GraphicsDevice;
            content = new IEContentManager(Services);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.graphicsDevice = GraphicsDevice;
        }

        public void PushGameScreen(GameScreen GameScreen)
        {
            // Only allow GameScreens to exist in one Engine at a time
            if (GameScreen.Engine != null)
                throw new Exception("This GameScreen already exists on the stack " +
                    " of another Engine instance");

            if (!gameScreens.Contains(GameScreen))
            {
                gameScreens.Add(GameScreen);
                GameScreen.engine = this;
                GameScreen.LoadGameScreen();
            }
        }

        public GameScreen PopGameScreen()
        {
            if (gameScreens.Count == 0)
                return null;

            // Stop linking the screen to this Engine instance before removing
            // the screen
            gameScreens[gameScreens.Count - 1].engine = null;

            // Return the top GameScreen from the stack
            return gameScreens[gameScreens.Count - 1];
        }

        public void Update(GameTime GameTime)
        {
            this.gameTime = GameTime;

            List<GameScreen> copy = new List<GameScreen>();

            foreach (GameScreen screen in gameScreens)
                copy.Add(screen);

            foreach (GameScreen screen in copy)
                screen.Update();
        }

        public void Draw(GameTime GameTime)
        {
            this.gameTime = GameTime;

            foreach (GameScreen screen in gameScreens)
                screen.Draw();
        }
    }
}
