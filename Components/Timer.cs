using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace Xanx.Components
{
    class Timer : Component
    {
        public bool finished = false;

        private double endTime;
        private Func<int> target;

        public Timer()
        {
        }

        public void SetTimer(TimeSpan time, Func<int> target)
        {
            this.endTime = Parent.Engine.GameTime.TotalGameTime.TotalMilliseconds + time.TotalMilliseconds;
            this.target = target;
        }

        public override void Update()
        {
            if (Parent.Engine.GameTime.TotalGameTime.TotalMilliseconds >= this.endTime)
            {
                this.target.Invoke();
                this.finished = true;
            }

            base.Update();
        }
    }
}
