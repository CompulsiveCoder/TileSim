using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine
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
            AddWater ();
            AddFood ();
        }

        public void AddTilesFromSettings()
        {
            var tiles = TileCreator.Create (World.Context.Settings.HorizontalTileCount, World.Context.Settings.VerticalTileCount);

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


        public void AddWater()
        {
            var waterPerTile = World.Context.Settings.DefaultWaterPerTile;

            AddWaterToTiles (waterPerTile);
        }

        public void AddWaterToTiles(decimal waterPerTile)
        {
            foreach (var tile in World.Tiles)
            {
                AddWaterToTile (tile, waterPerTile);
            }
        }

        public void AddWaterToTile(GameTile tile, decimal amountOfWater)
        {
            var randomNumber = Random.Next (2);

            if (randomNumber < amountOfWater) {
                tile.Inventory [ItemType.Water] += amountOfWater;
            }
        }

        public void AddFood()
        {
            var foodPerTile = World.Context.Settings.DefaultFoodPerTile;

            AddFoodToTiles (foodPerTile);
        }

        public void AddFoodToTiles(decimal foodPerTile)
        {
            foreach (var tile in World.Tiles)
            {
                AddFoodToTile (tile, foodPerTile);
            }
        }

        public void AddFoodToTile(GameTile tile, decimal amountOfFood)
        {
            var randomNumber = Random.Next (2);

            if (randomNumber < amountOfFood) {
                tile.Inventory [ItemType.Food] += amountOfFood;
            }
        }
    }
}

