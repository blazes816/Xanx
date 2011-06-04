using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Xanx.Components
{
    class Sprite : Component
    {
        private Texture2D spriteTexture;
        private string filename;

        private Vector2 velocity = Vector2.Zero;
        private Vector2 position = Vector2.Zero;

        public Sprite(float x, float y, string filename)
        {
            this.position = new Vector2(x, y);
            this.filename = filename;
        }

        public Sprite(Vector2 position, string filename)
        {
            this.position = position;
            this.filename = filename;
        }

        protected override void Load()
        {
            spriteTexture = Parent.Engine.Content.Load<Texture2D>(filename);
        }

        public override void Draw()
        {
            
            Parent.Engine.SpriteBatch.Begin(SpriteSortMode.Immediate,BlendState.Additive);
            Parent.Engine.SpriteBatch.Draw(spriteTexture, position, Color.White);
            Parent.Engine.SpriteBatch.End();
        }

        #region Getters & Setters
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public int Width { get { return spriteTexture.Width; } }
        public int Height { get { return spriteTexture.Height; } }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Texture2D SpriteTexture
        {
            get { return spriteTexture; }
            set { spriteTexture = value; }
        }
        #endregion
    }
}
