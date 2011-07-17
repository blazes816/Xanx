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
        private Vector2 acceleration = Vector2.Zero;
        private Vector2 position = Vector2.Zero;
        private Vector2 origin = Vector2.Zero;
        private Rectangle mask = Rectangle.Empty;
        private float rotation = 0;
        private float scale = 1;
        private float depth = 1;

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
            this.mask = new Rectangle(0,0, spriteTexture.Width, spriteTexture.Height);
        }

        public override void Draw()
        {
            
            Parent.Engine.SpriteBatch.Begin(SpriteSortMode.Immediate,BlendState.Additive);
            Parent.Engine.SpriteBatch.Draw(spriteTexture, this.Position, this.Mask, Color.White,
                this.Rotation, this.Origin, this.Scale, SpriteEffects.None, this.Depth);
            Parent.Engine.SpriteBatch.End();
        }

        public override void Update()
        {
            this.Velocity += this.Acceleration;
            this.Position += this.Velocity;
            base.Update();
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

        public Vector2 Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }

        public Texture2D SpriteTexture
        {
            get { return spriteTexture; }
            set { spriteTexture = value; }
        }

        public Rectangle Mask
        {
            get { return mask; }
            set { mask = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public float Depth
        {
            get { return depth; }
            set { depth = value; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        #endregion
    }
}
