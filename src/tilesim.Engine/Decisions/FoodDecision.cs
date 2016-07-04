using System;
using tilesim.Entities;

namespace tilesim.Engine
{
	public class FoodDecision : BaseDecision
	{
		public FoodDecision (EngineContext context) : base(context)
		{
		}

		public override void Decide(Person person)
		{
			throw new NotImplementedException ();
			/*if (person.Hunger > 50 // TODO: Make configurable
				&& person.Supplies[SupplyTypes.Food] > 0)
				person.Assign(ActivityType.Eating);
			else {
				if (person.Tile.RipeVegetables.Length > 0)
					person.Assign(ActivityType.Harvesting);
				else
					person.Assign(ActivityType.Gardening);
			}

			return person.ActivityType;*/
		}
	}
}

