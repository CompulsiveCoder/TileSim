using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class WaterDecision : BaseDecision
	{
		public decimal MinimumDrinkableAmount = 1;

		public WaterDecision (EngineContext context) : base(context)
		{
		}

		public override void Decide(Person person)
		{
			throw new NotImplementedException ();
			/*if (person.Is(ActivityType.Drinking)) {
				// TODO: Remove if not needed. Should be obsolete
				//if (person.Thirst == 0) {
				//	throw new NotImplementedException ();
				//	person.FinishActivity ();
				//}
			}
			else if (PersonNeedsToDrink (person))
				person.Assign(ActivityType.Drinking);
			else if (PersonIsCollectingWater (person))
			{
				// TODO: Remove if not needed. Should be obsolete
				//if (PersonHasFilledWater (person)) {
				//	throw new NotImplementedException ();
				//	person.FinishActivity ();
				//}
			}
			else if (CouldCollectMoreWater (person))
				person.Assign(ActivityType.CollectingWater);
			
			return person.ActivityType;*/
		}

		public bool PersonIsCollectingWater(Person person)
		{
			throw new NotImplementedException ();
			/*var isCurrentlyCollectingWater = person.ActivityType == ActivityType.CollectingWater;

			return isCurrentlyCollectingWater;*/
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

