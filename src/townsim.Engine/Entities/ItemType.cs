using System;

namespace townsim.Engine.Entities
{
	public enum ItemType
	{
		Shelter,
		Food,
		Water,
		Wood,
		Timber,

        // Person specific
        Drink, // TODO: Having Drink as well as Water is a hack to get both the DrinkWater and CollectWater activities working together. See if there's a better way to do it.
        Meal
	}
}

