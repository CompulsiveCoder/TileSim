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

        public GameTile[] Create(int numberOfTiles)
        {
            var list = new List<GameTile> ();

            for (int i = 0; i < numberOfTiles; i++) {
                var tile = CreateTile ();

                list.Add (tile);
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

