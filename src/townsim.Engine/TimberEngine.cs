using System;
using townsim.Entities;
using System.Collections.Generic;

namespace townsim.Engine
{
	public class TimberEngine
	{
		/// <summary>
		/// The amount of wastage when refining materials.
		/// </summary>
		public int WasteMultiplier = 10;

		public int TimberRate = 1;

		public TimberEngine ()
		{
		}

		public bool IsTimberAvailable(Town town, Building building)
		{
			return town.Forest.Length > 0;
		}

		public void RefineTimber(Town town, int timberQuantity)
		{
			var forestQuantity = timberQuantity * WasteMultiplier;

			var amount = forestQuantity;
			if (town.Forest.Length < amount)
				amount = (int)town.Forest.Length;

			double refinedTimber = 0;
			bool timberAvailable = true;
			while (refinedTimber < amount
				&& timberAvailable) {
				var amountOfTimber = MillTree (town);
				if (amountOfTimber == 0)
					timberAvailable = false;
				refinedTimber += amountOfTimber;
			}

			//town.Forest -= amount;

			//town.Timber += amount;
		}


		public void RefineTimber(Town town, Building building)
		{
			var amount = building.TimberPending;

			RefineTimber (town, building.TimberPending);

			town.Timber -= amount;
			building.TimberAvailable += amount;
		}

		public double MillTree(Town town)
		{
			Plant tree = null;
			foreach (var plant in town.Plants) {
				if (plant.Type == PlantType.Tree
				    && plant.Size > 10) {
					tree = plant;
					break;
				}
			}

			if (tree != null) {
				var timber = tree.Size * TimberRate;

				var list = new List<Plant> (town.Plants);
				list.Remove (tree);
				town.Plants = list.ToArray ();

				town.Timber += timber;

				return timber;
			} else
				return 0;
		}

	}
}

