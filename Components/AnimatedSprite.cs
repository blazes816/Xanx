using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xanx.Components
{
    class AnimatedSprite : Sprite
    {
        private int frame = 0;
        private int frames = 0;
        private float fps = 0;
        private float timePerFrame = 0;
        private float timeElapsed = 0;
        private float frameWidth = 0;
        private bool paused = true;

        public float Rotation, Scale, Depth;

        public AnimatedSprite(float x, float y, string filename) : base()
        {
        }

        protected override void Load()
        {
            base.Load();
            this.timePerFrame = (float)1 / this.fps;
            this.frameWidth = (float)spriteTexture.Bounds.Width / frames;
        }

        protected override void Update()
        {
            if (this.Paused)
                return;

            totalElapsed += Parent.Engine.GameTime.ElapsedGameTime.TotalSeconds;
            if (totalElapsed > timePerFrame)
            {
                this.frame++;
                frame = frame % frames;
                totalElapsed -= timePerFrame;
            }

            base.Update();
        }

        public void UpdateFrame(float elapsed)
        {
        }

        public void Reset()
        {
            frame = 0;
            totalElapsed = 0f;
        }
        public void Stop()
        {
            Pause();
            Reset();
        }
        public void Play()
        {
            this.Paused = false;
        }
        public void Pause()
        {
            this.Paused = true;
        }

        #region Getters & Setters
        public bool Paused
        {
            get { return this.paused; }
            set { this.paused = value; }
        }

        public Rectangle Mask
        {
            get { return mask; }
            set
            {
                new Rectangle(this.frameWidth * this.frame, 0, this.rameWidth, this.spriteTexture.Height);
            }
        }
        #endregion

    }
}
