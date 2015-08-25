using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class TimberEngine
	{
		/// <summary>
		/// The amount of wastage when refining materials.
		/// </summary>
		public int WasteMultiplier = 10;

		public TimberEngine ()
		{
		}

		public bool IsTimberAvailable(Town town, Building building)
		{
			return town.Forest > 0;
		}

		public void RefineTimber(Town town, int timberQuantity)
		{
			var forestQuantity = timberQuantity * WasteMultiplier;

			var amount = forestQuantity;
			if (town.Forest < amount)
				amount = (int)town.Forest;

			town.Forest -= amount;

			town.Timber += amount;
		}


		public void RefineTimber(Town town, Building building)
		{
			var amount = building.TimberNeeded;

			RefineTimber (town, building.TimberNeeded);

			building.TimberNeeded -= amount;
			building.TimberAvailable += amount;
		}

	}
}

