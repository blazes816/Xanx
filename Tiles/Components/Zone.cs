using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xanx.Tiles.Components
{
    class Zone : Component
    {
        private int[][] map;

        public Zone(int[][] map)
        {
            this.map = map;
        }
    }
}
