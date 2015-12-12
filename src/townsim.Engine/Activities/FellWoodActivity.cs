using System;
using townsim.Entities;
using System.Collections.Generic;
using townsim.Data;

namespace townsim.Engine.Activities
{
	[Serializable]
	public class FellWoodActivity : BaseActivity
	{
		public decimal ChoppingRate = 10m;

		public decimal TotalWoodFelled = 0;

		public FellWoodActivity (Person person, EngineSettings settings) : base(person, settings)
		{
		}

		public override void ExecuteSingleCycle ()
		{
			if (Person.ActivityType == ActivityType.FellWood) {
				//if (Person.HasDemand (SupplyTypes.Wood)) {
					//if (IsTimberAvailable (Person.Town))
					//FellWood (Person, Person.GetDemandAmount (SupplyTypes.Wood));


				if (IsComplete ())
					Finish ();
				else if (!TreesAvailableInTown ())
					Cancel ();
				else
					FellWood ();
				//}
			}
		}

		public bool IsTimberAvailable(Town town)
		{
			return town.Timber > 0;
		}

		/*
		 * public void FellWood(Person person, decimal totalNeeded)
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

		public decimal FellWood()
		{
			Plant tree = GetTargetTree ();

			if (tree != null) {
				var amountOfWood = tree.Size;

				SetTarget(tree);

				if (FellTreeCycle (Person, tree))
					FinishedFellingTree (Person, tree);

				return amountOfWood;
			} else
				return 0;
		}

		public bool FellTreeCycle(Person person, Plant plant)
		{
			plant.PercentHarvested += ChoppingRate;
			TotalWoodFelled += ChoppingRate;

			if (Settings.OutputType == ConsoleOutputType.General) {
				Console.WriteLine (plant.PercentHarvested + "%");
			}

			if (plant.PercentHarvested > 100)
				plant.PercentHarvested = 100;

			return plant.PercentHarvested == 100;
		}

		public void FinishedFellingTree(Person person, Plant tree)
		{
			var list = new List<Plant> (person.Town.Plants);
			list.Remove (tree);
			person.Town.Plants = list.ToArray ();

			var amountOfWood = tree.Size;

			Person.Supplies [SupplyTypes.Wood] = Person.Supplies [SupplyTypes.Wood] + amountOfWood;

			Person.RemoveDemand (SupplyTypes.Wood, amountOfWood);

			ClearTarget ();

			if (CurrentEngine.PlayerId == person.Id)
				LogWriter.Current.AppendLine (CurrentEngine.Id, String.Format("Player cut a tree down. Age:{0} size:{1} wood:{2}", (int)tree.Age, (int)tree.Size, (int)amountOfWood));

		}

		public void FinishedFellingWood(Person person)
		{
			Person.FinishActivity ();
		}

		public Plant GetLargeTree(Person person)
		{
			foreach (var plant in person.Town.Plants) {
				if (plant.Type == PlantType.Tree
					&& plant.Size > 10) {
					return plant;
				}
			}
			return null;
		}

		public Plant GetTargetTree()
		{
			Plant tree = null;

			if (Person.ActivityTarget is Plant
				&& ((Plant)Person.ActivityTarget).Type == PlantType.Tree)
				tree = (Plant)Person.ActivityTarget;
			else {
				if (Settings.OutputType == ConsoleOutputType.General) {
					Console.WriteLine ("Felling tree");
				}
				tree = GetLargeTree (Person);
			}

			return tree;
		}

		public override void Start ()
		{
		}

		public override bool IsComplete ()
		{
			return !Person.HasDemand(SupplyTypes.Wood);
		}

		public override bool IsImpossible ()
		{
			var woodNeeded = Person.GetDemandAmount (SupplyTypes.Wood);

			return Person.Town.WoodAvailableAsTrees < woodNeeded;
		}

		public override void Finish ()
		{
			CleanUp ();
		}

		public bool PersonHasEnoughWood(decimal amountOfWood)
		{
			var value = Person.Supplies [SupplyTypes.Wood] >= amountOfWood;

			return value;
		}

		public bool TreesAvailableInTown()
		{
			return Person.Town.Trees.Length > 0;
		}
	}
}

