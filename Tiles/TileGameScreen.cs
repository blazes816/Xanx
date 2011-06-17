using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;

using Xanx.Tiles.Components;

namespace Xanx.Tiles
{
    class TileGameScreen : GameScreen
    {
        private int tileWidth;
        private int tileHeight;
        private List<Zone> zones = new List<Zone>();

        public TileGameScreen(int width, int height) : base(width, height)
        {
            //DataContractJsonSerializer json = new DataContractJsonSerializer();

            // All of this should be serialized into the tile.manifest file
                int tileWidth = 64;
                int tileHeight = 64;

                
                // Create tiles
                string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory()+"\\Content\\Tiles", "*.xnb",
                                                        SearchOption.AllDirectories);

                foreach(string path in filePaths)
                {
                    string[] pieces = path.Split('\\');

                }

                // Create zones
                this.zones.Add(new Zone(new int[][]{
                    new int[] {0, 0, 0, 0, 0, 0, 0, 0},
                    new int[] {0, 0, 0, 0, 0, 0, 0, 0},
                    new int[] {0, 0, 0, 0, 0, 0, 0, 0},
                    new int[] {0, 0, 0, 0, 0, 0, 0, 0},
                    new int[] {0, 0, 0, 0, 0, 0, 0, 0},
                    new int[] {0, 0, 0, 0, 0, 0, 0, 0},
                    new int[] {0, 0, 0, 0, 0, 0, 0, 0},
                }));
            // End stuff to move to tile.manifest

            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
        }
    }
}
