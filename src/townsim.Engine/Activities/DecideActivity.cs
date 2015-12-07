using System;
using townsim.Entities;

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
			if (person.Activity == ActivityType.Inactive) {
				ChooseActivity (person);
			}
		}

		public void ChooseActivity(Person person)
		{

			if (PersonIsHomeless(person)) {
				person.Start (ActivityType.Builder);
			} else {
				ChooseActivityRandomly (person);
			}


		}

		public bool PersonIsHomeless(Person person)
		{
			return person.Home == null
				|| !person.Home.IsCompleted;
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

