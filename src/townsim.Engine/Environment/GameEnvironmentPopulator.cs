using System;
using townsim.Engine.Entities;

namespace townsim.Engine
{
    [Serializable]
    public class GameEnvironmentPopulator
    {
        public GameEnvironment World { get;set; }

        public TileCreator TileCreator { get; set; }
        public PersonCreator PersonCreator { get; set; }
        public PlantCreator PlantCreator { get; set; }

        public Random Random = new Random();

        public GameEnvironmentPopulator (GameEnvironment world)
        {
            World = world;


            // TODO: Should all these creators be consistent with their constructor parameters?
            TileCreator = new TileCreator (world);
            PersonCreator = new PersonCreator (world.Context.Settings);
            PlantCreator = new PlantCreator (world.Context.Settings);
        }

        public void PopulateFromSettings()
        {
            AddTilesFromSettings ();
            AddPeopleFromSettings ();
            AddTrees ();
        }

        public void AddTilesFromSettings()
        {
            var tiles = TileCreator.Create (World.Context.Settings.DefaultTileQuantity);

            World.Tiles = tiles;
        }

        public void AddPeopleFromSettings()
        {
            var peoplePerTile = World.Context.Settings.DefaultPeoplePerTile;

            AddPeopleToTiles (peoplePerTile);
        }

        public void AddPeopleToTiles(decimal peoplePerTile)
        {
            foreach (var tile in World.Tiles)
            {
                AddPeopleToTile (tile, peoplePerTile);
            }
        }

        public void AddPeopleToTile(GameTile tile, decimal numberOfPeople)
        {
            var randomNumber = Random.Next (2);

            if (randomNumber < numberOfPeople) {
                var people = PersonCreator.CreateAdults ((int)numberOfPeople);

                tile.AddPeople (people);
            }
        }

        public void AddTrees()
        {
            var treesPerTile = World.Context.Settings.DefaultTreesPerTile;

            AddTreesToTiles (treesPerTile);
        }

        public void AddTreesToTiles(decimal treesPerTile)
        {
            foreach (var tile in World.Tiles)
            {
                AddTreesToTile (tile, treesPerTile);
            }
        }

        public void AddTreesToTile(GameTile tile, decimal numberOfTrees)
        {
            var randomNumber = Random.Next (2);

            if (randomNumber < numberOfTrees) {
                var trees = PlantCreator.CreateTrees ((int)numberOfTrees);

                tile.AddTrees (trees);
            }
        }
    }
}

