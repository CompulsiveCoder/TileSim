using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class ChoiceEngine
	{
		public Random Randomizer = new Random();

		public ChoiceEngine ()
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

