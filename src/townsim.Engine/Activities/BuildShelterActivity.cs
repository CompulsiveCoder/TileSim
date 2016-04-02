using System;
using townsim.Engine.Entities;
using townsim.Engine.Needs;

namespace townsim.Engine.Activities
{
	[Activity(ItemType.Shelter)]
	public class BuildShelterActivity : BaseActivity
	{
		// TODO: Move to settings
		public int TimberCost = 50;

		public Building Shelter;

		// TODO
		//public FellWoodActivity WoodFelling;
		//public MillTimberActivity TimberMilling;

		public BuildShelterActivity (Person actor, NeedEntry needEntry, EngineSettings settings) : base(actor, needEntry, settings)
		{
			// TODO
			//WoodFelling = new FellWoodActivity ();
			//TimberMilling = new MillTimberActivity ();
		}

		public override bool CheckFinished ()
		{
            var isFinished = (Shelter != null && Shelter.IsCompleted);

            if (isFinished)
                ConfirmProduced (NeedEntry);

            return isFinished;
		}

		public override void Prepare (Person person)
		{
			throw new NotImplementedException ();
		}

        public override bool CheckSupplies (Person person)
        {
            if (ResourcesNeeded (person)) {
                RegisterNeedForTimber (person, TimberCost);
                return false;
            } else
                return true;
        }

		public override void Execute(Person person)
		{
			var buildStatus = GetBuildStatus (person);

			switch (buildStatus) {
			//case BuildStatus.PendingResources: // TODO: Remove if not needed. Should be obsolete
			//	if (Settings.IsVerbose)
			//		Console.WriteLine ("      The person does not have enough timber.");
			//	RegisterNeedForTimber (person);
			//	//GetTimber (person);
			//	break;
			case BuildStatus.NotYetStarted:
				StartConstruction (person);
				break;
			case BuildStatus.UnderConstruction:
				ContinueConstruction (person);
				break;
			//case BuildStatus.Completed: // TODO: Remove if not needed. Should be obsolete now
			//	if (Settings.IsVerbose) Console.WriteLine("Construction is complete.");
			//	break;
			}
			/*// TODO: Move this to a better loation

			if (HasEnoughTimber (person)) {
				ExecuteBuild (person);
			} else
				RegisterNeedForTimber (person);*/
		}

		public void StartConstruction(Person person)
		{
			if (Settings.IsVerbose)
				Console.WriteLine ("  Starting shelter construction");

			person.Home = new Building (BuildingType.House);

            Shelter = person.Home;

			TransferTimber (person, person.Home); // TODO: Should this transfer happen entirely here? Or happen incrementally later?
		}

		public void ContinueConstruction(Person person)
		{
			if (Settings.IsVerbose)
				Console.WriteLine ("  Continuing shelter construction");

			var home = person.Home;

            home.PercentComplete += PercentageValidator.Validate (Settings.ConstructionRate);

            home.PercentComplete = PercentageValidator.Validate (home.PercentComplete);

            if (Settings.IsVerbose) {
                Console.WriteLine ("    Increase: " + PercentageValidator.Validate(Settings.ConstructionRate) + "%");
                Console.WriteLine ("    Total: " + home.PercentComplete + "%");
            }
		}

		public void CheckComplete(Building home)
		{
            throw new NotImplementedException ();
            /*home.PercentComplete

			if (home.PercentComplete == 100) {
				home.IsCompleted = true;

				Finish ();
			}*/
		}

		public void GetTimber(Person person)
		{
			throw new NotImplementedException ();
			/*var timberRequired = TimberCost;

			var woodRequired = timberRequired * Settings.WoodRequiredForTimber;
							
			var millTimberActivity = new MillTimberActivity (person, Settings);
			millTimberActivity.SetQuantity (timberRequired);
			person.RushActivity (millTimberActivity);

			if (Settings.IsVerbose)
				Console.WriteLine ("        Getting timber by starting " + millTimberActivity.GetType().Name + " activity");
			*/
		}

		public bool BuildingHasEnoughTimber(Building building)
		{
			return building.TimberPending > 0;
		}

		public bool PersonHasEnoughTimber(Person person)
		{
			return person.Has (ItemType.Timber, TimberCost);
		}

        public void RegisterNeedForTimber(Person person, decimal amountOfTimber)
		{
			if (Settings.IsVerbose)
                Console.WriteLine ("        Registering the need for " + amountOfTimber + " timber");
			
            person.AddNeed (ItemType.Timber, amountOfTimber, 101);
		}

		public BuildStatus GetBuildStatus(Person person)
		{
            // TODO: Is this check here needed?
			if (ResourcesNeeded(person))
				return BuildStatus.PendingResources;

			if (person.Home == null)
				return BuildStatus.NotYetStarted;

            // TODO: Is this check here needed?
			if (!person.Home.IsCompleted)
				return BuildStatus.UnderConstruction;

			return BuildStatus.Completed;
		}

		public bool ResourcesNeeded(Person person)
		{
			var personHasTimber = PersonHasEnoughTimber (person);
			var buildingHasTimber = person.Home != null && !BuildingHasEnoughTimber (person.Home);

			return !personHasTimber && !buildingHasTimber;
		}

		public void TransferTimber(Person person, Building building)
		{
			if (Settings.IsVerbose)
				Console.WriteLine ("Transferring " + building.TimberPending + " timber from person to building.");


			// TODO: Clean up
			//if (Context.Settings.OutputType == ConsoleOutputType.Debug
			//	&& Context.Settings.PlayerId == person.Id) {
			//	Console.WriteLine ("Transferring " + building.TimberPending + " timber from person to building.");
			//}
			person.Supplies [ItemType.Timber] = person.Supplies [ItemType.Timber] - building.TimberPending;
			building.Timber += building.TimberPending;
		}
	}
}

