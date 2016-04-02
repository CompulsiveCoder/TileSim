using System;
using townsim.Engine.Entities;
using System.Collections.Generic;
using townsim.Data;
using townsim.Engine.Needs;

namespace townsim.Engine.Activities
{
	[Activity(ItemType.Wood)]
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

		public FellWoodActivity (Person person, NeedEntry needEntry, EngineSettings settings)
			: base(person, needEntry, settings)
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
				Console.WriteLine ("Starting " + GetType ().Name + " activity.");
				Console.WriteLine ("  Quantity (wood): " + NeedEntry.Quantity);
			}
			
			Plant treeBeingFelled = GetTreeToFell ();

			if (treeBeingFelled != null) {
				var amountOfWood = treeBeingFelled.Size;

				var isFinishedFellingTree = FellTreeCycle (Actor, treeBeingFelled);

				if (isFinishedFellingTree)
					FinishedFellingSingleTree (Actor, treeBeingFelled);

				// TODO: Remove if not needed
				//return amountOfWood;
			} else
				throw new Exception ("Can't fell wood when no trees are available.");
		}

        public override bool CheckSupplies (Person actor)
        {
            // TODO: Should an axe be a required supply?
            return true;
        }

		// TODO: Clean up
		//protected override void ExecuteSingleCycle ()
		//{
		//	FellWoodCycle ();
		//}

		public bool IsTimberAvailable(Town town)
		{
			return town.Timber > 0;
		}

		// TODO: Clean up
		/*public void FellWood(Person person, decimal totalNeeded)
		{
			if (!PersonHasEnoughWood(totalNeeded)
				&& TreesAvailableInTown()) {

				if (!IsComplete())
					FellTree (person);
				else
					Finish()
			} else {
				person.FinishActivity ();
			}
		}*/

		public bool FellTreeCycle(Person person, Plant plant)
		{
			if (Settings.IsVerbose)
				Console.WriteLine ("  Felling tree");

			plant.PercentHarvested += Settings.FellingRate;
			TotalWoodFelled += Settings.FellingRate; // TODO: Should this be set here or once the tree is finished?

			if (plant.PercentHarvested > 100)
				plant.PercentHarvested = 100;

			if (Settings.OutputType == ConsoleOutputType.Debug) {
				Console.WriteLine ("  " + plant.PercentHarvested + "% felled");
			}

			if (plant.PercentHarvested > 100) {
				plant.PercentHarvested = 100;
			}

			if (plant.PercentHarvested == 100
				&& Settings.IsVerbose) {
				Console.WriteLine ("  Finished felling tree.");
			}
				

			return plant.PercentHarvested == 100;
		}

		public void FinishedFellingSingleTree(Person person, Plant tree)
		{
			person.Tile.RemovePlant (tree);

			var amountOfWood = tree.Size;

			NeedsProduced [ItemType.Wood] = amountOfWood;
			//Actor.AddSupply(NeedType.Wood, amountOfWood);

			if (Settings.IsVerbose) {
				Console.WriteLine ("  Wood from tree: " + amountOfWood);
				Console.WriteLine ("  Total wood: " + person.Supplies [ItemType.Wood]);
			}

			Target = null;

			// TODO: Clean up
			//ClearTarget ();

			//if (Context.Settings.PlayerId == person.Id)
				//Context.Log.WriteLine (String.Format("Player cut a tree down. Age:{0} size:{1} wood:{2}", (int)tree.Age, (int)tree.Size, (int)amountOfWood));
		}

		public void FinishedFellingRequiredWood(Person person)
		{
			throw new NotImplementedException ();
			Finish ();
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
						Console.WriteLine (" Found large tree");
					
					return plant;
				}
			}

			if (Settings.IsVerbose)
				Console.WriteLine ("  No large trees available");
			
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
					Console.WriteLine ("  Cutting down tree");
				}
			}

			return tree;
		}

		public void Start ()
		{
			throw new NotImplementedException ();
		}

		public bool CheckComplete ()
		{
			throw new NotImplementedException ();

			//var value = !Person.HasDemand(NeedType.Wood);
			//return value;
		}

		public bool CheckImpossible ()
		{

			throw new NotImplementedException ();
			return !TreesAvailableInTown ();
			/*var woodNeeded = Person.GetDemandAmount (NeedType.Wood);

			return Person.Town.WoodAvailableAsTrees < woodNeeded;*/
		}

		public bool PersonHasEnoughWood(decimal amountOfWood)
		{
			throw new NotImplementedException ();

			//var value = Person.Supplies [NeedType.Wood] >= amountOfWood;

			//return value;
		}

		public bool TreesAvailableInTown()
		{
			throw new NotImplementedException ();
			//return Person.Town.Trees.Length > 0;
		}
	}
}

