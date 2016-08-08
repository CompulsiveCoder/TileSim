using System;
using tilesim.Engine.Entities;
using System.Collections.Generic;

namespace tilesim.Engine
{
    [Serializable]
    public class TileCreator
    {
        public GameEnvironment World { get; set; }

        public TileCreator (GameEnvironment world)
        {
            World = world;
        }

        public GameTile[] Create(int horizontalTileCount, int verticalTileCount)
        {
            var list = new List<GameTile> ();

            for (int x = 0; x < horizontalTileCount; x++) {
                for (int y = 0; y < verticalTileCount; y++) {
                    var tile = CreateTile ();
                    tile.PositionX = x;
                    tile.PositionY = y;

                    list.Add (tile);
                }
            }

            return list.ToArray ();
        }

        public GameTile CreateTile()
        {
            var tile = new GameTile (World);

            return tile;
        }
    }
}

