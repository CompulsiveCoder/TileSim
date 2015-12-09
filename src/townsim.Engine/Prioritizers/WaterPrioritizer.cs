using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class WaterPrioritizer
	{
		public WaterPrioritizer ()
		{
		}

		public void Prioritize(Person person)
		{
			PrioritizeCritical (person);

			PrioritizeSecondary (person);
		}

		public void PrioritizeCritical(Person person)
		{
			person.Priorities [PriorityTypes.Water] = person.Thirst;

			// TODO: Should this be here?
			if (person.Supplies[SupplyTypes.Water] > person.SuppliesMax[SupplyTypes.Water]/2)
				person.Priorities [PriorityTypes.Water] = person.Thirst/2;
		}

		public void PrioritizeSecondary(Person person)
		{
			if (!person.IsActive) {
				person.Priorities [PriorityTypes.Water] = 60;
			}
		}

	}
}

