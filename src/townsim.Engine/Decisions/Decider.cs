using System;
using townsim.Entities;
using System.Collections.Generic;
using townsim.Data;

namespace townsim.Engine.Decisions
{
	public class Decider
	{
		public Random Randomizer = new Random();

		public EngineSettings Settings { get; set; }

		public Decider (EngineSettings settings)
		{
			Settings = settings;
		}

		public void Decide(Person person)
		{
			var previousActivity = person.ActivityType;

			ChooseActivity (person);

			if (previousActivity != person.ActivityType) {
				if (Settings.OutputType == ConsoleOutputType.General) {
					Console.WriteLine ("Player chose activity: " + person.ActivityType);
				}
			}

		}

		public void ChooseActivity(Person person)
		{
			ChooseActivityBasedOnPriorities (person);
		}

		public bool PersonIsHomeless(Person person)
		{
			return person.Home == null
				|| !person.Home.IsCompleted;
		}

		public void ChooseActivityBasedOnPriorities(Person person)
		{
			var priorities = GetHighestPriorities (person);

			if (priorities.Length == 0)
				person.Start(ActivityType.Inactive);
			else {
				var index = 0;

				if (priorities.Length == 1) {
					index = 0;
				} else {
					index = new Random ().Next (0, priorities.Length);
				}

				ChooseActivityBasedOnPriority (person, priorities [index]);
			}
		}

		public void ChooseActivityBasedOnPriority(Person person, PriorityTypes priority)
		{
			var previousActivity = person.ActivityType;

			if (Settings.OutputType == ConsoleOutputType.General
				&& person.Id == CurrentEngine.PlayerId) {
				Console.WriteLine ("Priority: " + priority.ToString ());
			}

			if (priority == PriorityTypes.Shelter) {

				if (person.HasDemand(SupplyTypes.Timber))
				{
					if (person.HasDemand(SupplyTypes.Wood))
						new WoodDecision (Settings).Decide (person);
					else
						new TimberDecision (Settings).Decide (person);
				}
				else
					new ShelterDecision (Settings).Decide (person);
			}
			else if (priority == PriorityTypes.Water)
			{
				new WaterDecision (Settings).Decide (person);
			}
			else if (priority == PriorityTypes.Food)
			{
				new FoodDecision (Settings).Decide (person);
			}

			var activityHasChanged = previousActivity != person.ActivityType;

			if (activityHasChanged && CurrentEngine.PlayerId == person.Id) {
				switch (person.ActivityType) {
				case ActivityType.CollectingWater:
					LogWriter.Current.AppendLine (CurrentEngine.Id, "The player has started collecting water.");
					break;
				case ActivityType.Drinking:
					LogWriter.Current.AppendLine (CurrentEngine.Id, "The player has started drinking water.");
					break;
				case ActivityType.FellWood:
					LogWriter.Current.AppendLine (CurrentEngine.Id, "The player has started felling wood.");
					break;
				case ActivityType.MillTimber:
					LogWriter.Current.AppendLine (CurrentEngine.Id, "The player has started milling timber.");
					break;
				}
			}
		}

		public PriorityTypes[] GetHighestPriorities(Person person)
		{
			var possibleChoices = new List<PriorityTypes> ();
			var highestValue = 0m;

			foreach (var priority in person.Priorities) {
				if (priority.Value > 0) {
					if (priority.Value == highestValue) {
						possibleChoices.Add (priority.Key);
					}
					if (priority.Value > highestValue) {
						highestValue = priority.Value;
						possibleChoices.Clear ();
						possibleChoices.Add (priority.Key);
					}
				}
			}

			return possibleChoices.ToArray ();
		}
	}
}

