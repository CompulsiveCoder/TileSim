using System;
using townsim.Entities;
using System.Collections.Generic;

namespace townsim.Engine.Activities
{
	public class DecideActivity
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
				person.Activity = ActivityType.Inactive;
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
			if (priority == PriorityTypes.Shelter)
				person.Activity = ActivityType.Builder;
			else if (priority == PriorityTypes.Food)
			{
				if (person.Hunger > 80)
					person.Activity = ActivityType.Eating;
				else {
					if (person.Town.RipeVegetables.Length > 0)
						person.Activity = ActivityType.Harvesting;
					else
						person.Activity = ActivityType.Gardening;
				}
			}
			else if (priority == PriorityTypes.Water)
				person.Activity = ActivityType.Drinking;
			
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

