using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class FoodDecision
	{
		public FoodDecision ()
		{
		}

		public ActivityType Decide(Person person)
		{
			if (person.Hunger > 50 // TODO: Make configurable
				&& person.Supplies[SupplyTypes.Food] > 0)
				person.Activity = ActivityType.Eating;
			else {
				if (person.Town.RipeVegetables.Length > 0)
					person.Activity = ActivityType.Harvesting;
				else
					person.Activity = ActivityType.Gardening;
			}

			return person.Activity;
		}
	}
}

