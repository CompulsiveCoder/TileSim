using System;
using townsim.Engine.Entities;
using System.Collections.Generic;
using townsim.Data;
using townsim.Engine.Needs;

namespace townsim.Engine.Activities
{
    [Activity(ActionType.Fell, ItemType.Wood)]
	[Serializable]
	public class FellWoodActivity : BaseActivity
	{
		public decimal TotalWoodFelled = 0;

		public Plant Target { get;set; }

		public decimal AmountOfWoodToFell
		{
			get { return NeedEntry.Quantity; }
			set { NeedEntry.Quantity = value; }
		}

        public FellWoodActivity (Person person, NeedEntry needEntry, EngineSettings settings, ConsoleHelper console)
			: base(person, needEntry, settings, console)
		{
		}

		public override void Prepare (Person person)
		{
			throw new NotImplementedException ();
		}

		public override bool CheckFinished ()
		{
			return TotalWoodFelled >= AmountOfWoodToFell;
		}

		public override void Execute (Person person)
		{
			if (Settings.IsVerbose)
			{
				Console.WriteDebugLine ("Starting " + GetType ().Name + " activity.");
				Console.WriteDebugLine ("  Quantity (wood): " + NeedEntry.Quantity);
			}
			
			Plant treeBeingFelled = GetTreeToFell ();

			if (treeBeingFelled != null) {
				var amountOfWood = treeBeingFelled.Size;

				var isFinishedFellingTree = FellTreeCycle (Actor, treeBeingFelled);

				if (isFinishedFellingTree)
					FinishedFellingSingleTree (Actor, treeBeingFelled);
			} else
				throw new Exception ("Can't fell wood when no trees are available.");
		}

        public override bool CheckRequiredItems (Person actor)
        {
            // TODO: Implement the need for an axe
            return true;
        }

		public bool IsTimberAvailable(Town town)
		{
			return town.Timber > 0;
		}

		public bool FellTreeCycle(Person person, Plant plant)
		{
			if (Settings.IsVerbose)
				Console.WriteDebugLine ("  Felling tree");

			plant.PercentHarvested += Settings.FellingRate;
			TotalWoodFelled += Settings.FellingRate; // TODO: Should this be set here or once the tree is finished?

			if (plant.PercentHarvested > 100)
				plant.PercentHarvested = 100;

			if (Settings.OutputType == ConsoleOutputType.Debug) {
				Console.WriteDebugLine ("  " + plant.PercentHarvested + "% felled");
			}

			if (plant.PercentHarvested > 100) {
				plant.PercentHarvested = 100;
			}

			if (plant.PercentHarvested == 100
				&& Settings.IsVerbose) {
				Console.WriteDebugLine ("  Finished felling tree.");
			}
				

			return plant.PercentHarvested == 100;
		}

		public void FinishedFellingSingleTree(Person person, Plant tree)
		{
			person.Tile.RemovePlant (tree);

			var amountOfWood = tree.Size;

			ItemsProduced [ItemType.Wood] = amountOfWood;

			if (Settings.IsVerbose) {
				Console.WriteDebugLine ("  Wood from tree: " + amountOfWood);
                Console.WriteDebugLine ("  Total wood: " + person.Inventory.Items [ItemType.Wood] + amountOfWood);
			}

			Target = null;

			//if (Context.Settings.PlayerId == person.Id)
				//Context.Log.WriteLine (String.Format("Player cut a tree down. Age:{0} size:{1} wood:{2}", (int)tree.Age, (int)tree.Size, (int)amountOfWood));
		}

		public Plant FindLargeTree()
		{
			if (Actor.Tile == null)
				throw new Exception ("The Actor.Tile property is null. Set a tile first.");

			// TODO: Use linq
			foreach (var plant in Actor.Tile.Plants) {
				if (plant.Type == PlantType.Tree
					&& plant.Size > Settings.MinimumTreeSize) {

					if (Settings.IsVerbose)
						Console.WriteDebugLine (" Found large tree");
					
					return plant;
				}
			}

			if (Settings.IsVerbose)
				Console.WriteDebugLine ("  No large trees available");
			
			return null;
		}

		public Plant GetTreeToFell()
		{
			Plant tree = null;

			if (Target != null)
				tree = (Plant)Target;
			else {
				tree = FindLargeTree ();

				Target = tree;

				if (Settings.IsVerbose) {
					Console.WriteDebugLine ("  Cutting down tree");
				}
			}

			return tree;
		}
	}
}

