using System;
using townsim.Entities;
using townsim.Engine.Activities;
using townsim.Engine.Decisions;

namespace townsim.Engine
{
	public class PersonEngine
	{
		public EngineSettings Settings { get;set; }
		public EngineClock Clock { get;set; }

		public PersonEngine (EngineSettings settings, EngineClock clock)
		{
			Settings = settings;
			Clock = clock;
		}

		public void Start(Person person)
		{
			switch (person.ActivityType) {
			case ActivityType.Drinking:
				new DrinkActivity (person, Settings).Start ();
				break;
			case ActivityType.CollectingWater:
				new CollectWaterActivity (person, Settings).Start ();
				break;
			case ActivityType.Builder:
				new BuildActivity (person, Settings, Clock).Start ();
				break;
			case ActivityType.Eating:
				new EatActivity (person, Settings).Start ();
				break;
			case ActivityType.Harvesting:
				new HarvestActivity (person, Settings, Clock).Start ();
				break;
			case ActivityType.Gardening:
				new GardenActivity (person, Settings, Clock).Start ();
				break;
			case ActivityType.FellWood:
				new FellWoodActivity (person, Settings).Start ();
				break;
			case ActivityType.MillTimber:
				new MillTimberActivity (person, Settings).Start ();
				break;
			case ActivityType.PlantTrees:
				new PlantTreesActivity (person, Settings, Clock).Start ();
				break;
			case ActivityType.Inactive:
				// Don't do anything
				break;
			default:
				throw new Exception ("Unsupported activity: " + person.ActivityType);
			}
		}

		public void StartSingleCycle(Person person)
		{
			SetPriorities (person);

			MakeDecisions (person);

			if (person.Activity == null)
				Start (person);

			if (person.Activity != null)
				person.Activity.StartSingleCycle ();
		}

		public void SetPriorities(Person person)
		{
			var shelterPrioritizer = new ShelterPrioritizer ();
			var foodPrioritizer = new FoodPrioritizer ();
			var waterPrioritizer = new WaterPrioritizer ();

			waterPrioritizer.Prioritize (person);
			shelterPrioritizer.Prioritize (person);
			foodPrioritizer.Prioritize (person);
		}

		public void MakeDecisions(Person person)
		{
			var decider = new Decider (Settings);
			decider.Decide (person);
		}
	}
}

