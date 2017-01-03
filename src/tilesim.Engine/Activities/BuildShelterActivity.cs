using System;
using tilesim.Engine.Entities;
using tilesim.Engine.Needs;

namespace tilesim.Engine.Activities
{
    [Activity(ActivityVerb.Build, ItemType.Shelter, PersonVitalType.NotSet)]
	public class BuildShelterActivity : BaseActivity
	{
		public Building Shelter;

        public BuildShelterActivity (Person actor, NeedEntry needEntry, EngineSettings settings, ConsoleHelper console)
            : base(actor, needEntry, settings, console)
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

        public override bool IsActorAbleToAct (Person actor)
        {
            var isAble = !ResourcesNeeded (actor);

            if (!isAble)
                Console.WriteDebugLine ("  Actor doesn't have enough timber to build a shelter.");

            return isAble;
        }

        public override void RegisterNeeds (Person person)
        {
            if (ResourcesNeeded (person))
                RegisterNeedToMillTimber (person, Settings.ShelterTimberCost);
        }

		public override void Execute(Person person)
		{
            Actor = person;
            Console.WriteDebugLine ("Executing BuildShelterActivity");

			var buildStatus = GetBuildStatus (person);

            Console.WriteDebugLine ("  Build status: " + buildStatus);

			switch (buildStatus) {
			//case BuildStatus.PendingResources: // TODO: Remove if not needed. Should be obsolete
			//	if (Settings.IsVerbose)
			//		Console.WriteDebugLine ("      The person does not have enough timber.");
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
			//	if (Settings.IsVerbose) Console.WriteDebugLine("Construction is complete.");
			//	break;
			}
			/*// TODO: Move this to a better location or remove

			if (HasEnoughTimber (person)) {
				ExecuteBuild (person);
			} else
				RegisterNeedForTimber (person);*/
		}

		public void StartConstruction(Person person)
		{
            Status = "Starting construction";

			if (Settings.IsVerbose)
				Console.WriteDebugLine ("  Starting shelter construction");

			person.Home = new Building (BuildingType.House, Settings);

            Shelter = person.Home;

			TransferTimber (person, person.Home); // TODO: Should this transfer happen entirely here? Or happen incrementally later?
		}

		public void ContinueConstruction(Person person)
		{
			if (Settings.IsVerbose)
				Console.WriteDebugLine ("  Continuing shelter construction");

			var home = person.Home;

            Status = "Building " + (int)home.PercentComplete + "%";

            var constructionPercentageIncrease = Settings.ConstructionRate;

            home.IncreasePercentComplete(constructionPercentageIncrease);

            SetPercentComplete(home.PercentComplete);

            if (Settings.IsVerbose) {
                Console.WriteDebugLine ("    Increase: " + PercentageValidator.Validate(Settings.ConstructionRate) + "%");
                Console.WriteDebugLine ("    Total: " + home.PercentComplete + "%");
            }
		}

		public bool BuildingHasEnoughTimber(Building building)
		{
			return building.TimberPending > 0;
		}

		public bool PersonHasEnoughTimber(Person person)
		{
            return person.Inventory.Has (ItemType.Timber, Settings.ShelterTimberCost);
		}

        public void RegisterNeedToMillTimber(Person person, decimal amountOfTimber)
		{
            Console.WriteDebugLine ("        Registering the need to " + ActivityVerb.Mill + " " + amountOfTimber + " timber");
			
            AddNeed(ActivityVerb.Mill, ItemType.Timber, PersonVitalType.NotSet, amountOfTimber, NeedEntry.Priority+1);
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
            // TODO: Does the timber need to be assigned to both the person and the build? Shouldnt
			var personHasTimber = PersonHasEnoughTimber (person);
			var buildingHasTimber = person.Home != null && !BuildingHasEnoughTimber (person.Home);

			return !personHasTimber && !buildingHasTimber;
		}

		public void TransferTimber(Person person, Building building)
		{
			if (Settings.IsVerbose)
				Console.WriteDebugLine ("Transferring " + building.TimberPending + " timber from person to building.");


			// TODO: Clean up
			//if (Context.Settings.OutputType == ConsoleOutputType.Debug
			//	&& Context.Settings.PlayerId == person.Id) {
			//	Console.WriteDebugLine ("Transferring " + building.TimberPending + " timber from person to building.");
			//}

            Transfers.Add (new ItemTransfer (person, building, ItemType.Timber, building.TimberPending));
		}
	}
}

