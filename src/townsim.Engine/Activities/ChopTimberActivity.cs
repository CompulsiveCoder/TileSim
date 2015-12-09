using System;
using townsim.Entities;
using System.Collections.Generic;
using townsim.Data;

namespace townsim.Engine.Activities
{
	[Serializable]
	public class ChopTimberActivity : BaseActivity
	{
		/// <summary>
		/// The amount of wastage when refining materials.
		/// </summary>
		public int WasteMultiplier = 10;

		public int TimberRate = 1;

		public ChopTimberActivity ()
		{
		}

		public override void Act ()
		{
			throw new NotImplementedException ();
		}

		public bool IsTimberAvailable(Town town, Building building)
		{
			return town.Timber > 0;
		}

		public void MillTimber(Town town, int timberQuantity)
		{
			//var forestQuantity = timberQuantity * WasteMultiplier;

			//var numberOfTrees = forestQuantity;
			//if (town.Forest.Length < numberOfTrees)
			//	numberOfTrees = (int)town.Forest.Length;

			double refinedTimber = 0;
			bool timberAvailable = true;

			while (refinedTimber < timberQuantity
				&& timberAvailable) {
				var amountOfTimber = MillTree (town);
				if (amountOfTimber <= 0)
					timberAvailable = false;
				refinedTimber += amountOfTimber;
			}

			//town.Forest -= amount;

			//town.Timber += amount;
		}


		public void MillTimber(Town town, Building building)
		{
			var amount = building.TimberPending;

			MillTimber (town, building.TimberPending);

			// Move the timber from the town store to the building store
			town.Timber -= amount;
			building.Timber += amount;
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
				var timber = (int)(tree.Size * TimberRate);

				var list = new List<Plant> (town.Plants);
				list.Remove (tree);
				town.Plants = list.ToArray ();

				town.Timber += timber;

				LogWriter.Current.AppendLine (CurrentEngine.Id, String.Format("A tree was cut down. Age:{0} size:{1} timber:{2}", (int)tree.Age, (int)tree.Size, (int)timber));

				return timber;
			} else
				return 0;
		}

	}
}

