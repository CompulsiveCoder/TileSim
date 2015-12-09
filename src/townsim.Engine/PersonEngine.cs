﻿using System;
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
				new DrinkActivity (Settings).Start (person);
				break;
			case ActivityType.CollectingWater:
				new CollectWaterActivity (Settings).Start (person);
				break;
			case ActivityType.Builder:
				new BuildActivity (Settings, Clock).Start (person);
				break;
			case ActivityType.Eating:
				new EatActivity (Settings).Start (person);
				break;
			case ActivityType.Harvesting:
				new HarvestActivity (Settings, Clock).Start (person);
				break;
			case ActivityType.Gardening:
				new GardenActivity (Settings, Clock).Start (person);
				break;
			case ActivityType.Inactive:
				// Don't do anything
				break;
			default:
				throw new Exception ("Unsupported activity: " + person.ActivityType);
			}
		}

		public void Act(Person person)
		{
			SetPriorities (person);

			MakeDecisions (person);

			if (person.Activity == null)
				Start (person);

			if (person.Activity != null)
				person.Activity.Act ();
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
			var decider = new Decider ();
			decider.Decide (person);
		}
	}
}

