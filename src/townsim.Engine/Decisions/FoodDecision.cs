using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class FoodDecision : BaseDecision
	{
		public FoodDecision (EngineSettings settings) : base(settings)
		{
		}

		public override ActivityType Decide(Person person)
		{
			if (person.Hunger > 50 // TODO: Make configurable
				&& person.Supplies[SupplyTypes.Food] > 0)
				person.ActivityType = ActivityType.Eating;
			else {
				if (person.Town.RipeVegetables.Length > 0)
					person.ActivityType = ActivityType.Harvesting;
				else
					person.ActivityType = ActivityType.Gardening;
			}

			return person.ActivityType;
		}
	}
}

