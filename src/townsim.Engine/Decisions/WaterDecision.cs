using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class WaterDecision
	{
		public decimal MinimumDrinkableAmount = 1;

		public WaterDecision ()
		{
		}

		public ActivityType Decide(Person person)
		{
			if (person.Thirst > 80// TODO: Make this configurable
			    && person.Supplies [SupplyTypes.Water] > MinimumDrinkableAmount)
				person.ActivityType = ActivityType.Drinking;
			else {
				if (person.Supplies [SupplyTypes.Water] < MinimumDrinkableAmount
					&& person.Town.WaterSources > 0)
					person.ActivityType = ActivityType.CollectingWater;
			}
		
			return person.ActivityType;
		}
	}
}

