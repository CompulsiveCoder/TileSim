using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class WaterDecision : BaseDecision
	{
		public decimal MinimumDrinkableAmount = 1;

		public WaterDecision (EngineSettings settings) : base(settings)
		{
		}

		public override ActivityType Decide(Person person)
		{
			if (person.Is(ActivityType.Drinking)) {
				if (person.Thirst == 0) {
					person.FinishActivity ();
				}
			}
			else if (PersonNeedsToDrink (person))
				person.Start(ActivityType.Drinking);
			else if (PersonIsCollectingWater (person))
			{
				if (PersonHasFilledWater (person)) {
					person.FinishActivity ();
				}
			}
			else if (CouldCollectMoreWater (person))
				person.Start(ActivityType.CollectingWater);
			
			return person.ActivityType;
		}

		public bool PersonIsCollectingWater(Person person)
		{
			var isCurrentlyCollectingWater = person.ActivityType == ActivityType.CollectingWater;

			return isCurrentlyCollectingWater;
		}

		public bool PersonHasFilledWater(Person person)
		{
			return person.Supplies [SupplyTypes.Water] >= person.SuppliesMax [SupplyTypes.Water];
		}

		public bool PersonNeedsToDrink(Person person)
		{
			var isThirsty = person.Thirst > 50; // TODO: Make this configurable

			var hasWaterToDrink = person.Supplies [SupplyTypes.Water] >= MinimumDrinkableAmount;

			return isThirsty && hasWaterToDrink;
		}
			
		public bool CouldCollectMoreWater(Person person)
		{
			var personHasSpace = person.Supplies [SupplyTypes.Water] < person.SuppliesMax [SupplyTypes.Water];

			var townHasWater = person.Town.WaterSources > 0;

			return personHasSpace && townHasWater;
		}
	}
}

