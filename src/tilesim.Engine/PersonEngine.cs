using System;
using tilesim.Engine.Entities;
using tilesim.Engine.Activities;
using tilesim.Engine.Decisions;

namespace tilesim.Engine
{
	public class PersonEngine
	{
		public EngineContext Context { get;set; }

		public PersonDecider Decider { get; set; }

		public PersonEngine (EngineContext context)
		{
			Context = context;
			Decider = new PersonDecider (context);
		}

		public void Start(Person person)
		{
			throw new NotImplementedException ();

			/*switch (person.ActivityType) {
			case ActivityType.Drinking:
				new DrinkActivity (person, Context).Start ();
				break;
			case ActivityType.CollectingWater:
				new CollectWaterActivity (person, Context).Start ();
				break;
			case ActivityType.Builder:
				new BuildActivity (person, Context).Start ();
				break;
			case ActivityType.Eating:
				new EatActivity (person, Context).Start ();
				break;
			case ActivityType.Harvesting:
				new HarvestActivity (person, Context).Start ();
				break;
			case ActivityType.Gardening:
				new GardenActivity (person, Context).Start ();
				break;
			case ActivityType.FellWood:
				throw new NotImplementedException ();
				//new FellWoodActivity (person, Context).Start ();
				break;
			case ActivityType.MillTimber:
				new MillTimberActivity (person, Context).Start ();
				break;
			case ActivityType.PlantTrees:
				new PlantTreesActivity (person, Context).Start ();
				break;
			case ActivityType.Inactive:
				// Don't do anything
				break;
			default:
				throw new Exception ("Unsupported activity: " + person.ActivityType);
			}*/
		}

		public void StartSingleCycle(Person person)
		{
			if (Context.Settings.IsVerbose)
				Context.Console.WriteDebugLine ("Starting cycle for person");

			RegisterNeeds (person);

			MakeDecisions (person);

			PerformActivity (person);

			// TODO
			//RemoveObsoleteNeeds (person);

			/*SetPriorities (person);

			MakeDecisions (person);

			if (person.Activity == null)
				Start (person);

			if (person.Activity != null)
				person.Activity.StartSingleCycle ();*/
		}

		public void RegisterNeeds(Person person)
		{
			if (Context.Settings.IsVerbose)
				Context.Console.WriteDebugLine ("  Registering needs for person");

			foreach (var need in Context.World.Logic.Needs) {
				need.RegisterIfNeeded (person);
			}
		}

		// TODO: Remove if not needed
		/*public void SetPriorities(Person person)
		{
			var shelterPrioritizer = new ShelterPrioritizer ();
			var foodPrioritizer = new FoodPrioritizer ();
			var waterPrioritizer = new WaterPrioritizer ();

			waterPrioritizer.Prioritize (person);
			shelterPrioritizer.Prioritize (person);
			foodPrioritizer.Prioritize (person);
		}*/

		public void MakeDecisions(Person person)
		{
			if (Context.Settings.IsVerbose)
                Context.Console.WriteDebugLine ("  Making decisions for person");
			
			var activity = Decider.Decide (person);

            if (activity != null) {
                var userIsAlreadyPerformingActivity = (activity.Name == person.ActivityName);

                if (!userIsAlreadyPerformingActivity)
                    person.RushActivity (activity);
            }
		}

		public void PerformActivity(Person person)
		{
			var activity = person.Activity;

			if (Context.Settings.IsVerbose)
                Context.Console.WriteDebugLine ("  Performing activity: " + (activity != null ? activity.GetType().Name : "[idle]"));

			if (activity != null)
				activity.Act (person);
		}

		public void RemoveObsoleteNeeds(Person person)
		{
			throw new NotImplementedException ();

			/*foreach (var entry in person.Needs) {
				var existingAmount = person.Inventory.Items [entry.Type];
				if (existingAmount > entry.Quantity)
					person.Needs.Remove (entry);
			}*/
		}
	}
}

