using System;
using townsim.Entities;
using System.Collections.Generic;
using townsim.Data;

namespace townsim.Engine.Activities
{
	public class DecideActivity : BaseActivity
	{
		public Random Randomizer = new Random();

		public DecideActivity ()
		{
		}

		public void Update(Person person)
		{
			//if (person.Activity == ActivityType.Inactive) {
				ChooseActivity (person);
			//}
		}

		public void ChooseActivity(Person person)
		{

			//if (PersonIsHomeless(person)) {
			//	person.Start (ActivityType.Builder);
			//} else {
				ChooseActivityBasedOnPriorities (person);

				//ChooseActivityRandomly (person);
			//}


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
			var previousActivity = person.Activity;

			if (priority == PriorityTypes.Shelter)
				person.Start(ActivityType.Builder);
			else if (priority == PriorityTypes.Food)
			{
				new FoodDecision ().Decide (person);
			}
			else if (priority == PriorityTypes.Water)
			{
				new WaterDecision ().Decide (person);
			}

			var activityHasChanged = previousActivity != person.Activity;

			if (activityHasChanged && CurrentEngine.PlayerId == person.Id) {
				switch (person.Activity) {
				case ActivityType.CollectingWater:
					LogWriter.Current.AppendLine (CurrentEngine.Id, "The player has started collecting water.");
					break;
				case ActivityType.Drinking:
					LogWriter.Current.AppendLine (CurrentEngine.Id, "The player has started drinking water.");
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

		public void ChooseActivityRandomly(Person person)
		{
			// TODO: Implement priorities
			var activities = new string[] {
				"Builder",
				"Forestry",
				"Gardening",
				"Harvesting"
			};

			var randomIndex = Randomizer.Next (activities.Length);

			var randomizedActivity = (ActivityType)Enum.Parse(typeof(ActivityType), activities [randomIndex]);

			person.Start(randomizedActivity);
		}
	}
}

