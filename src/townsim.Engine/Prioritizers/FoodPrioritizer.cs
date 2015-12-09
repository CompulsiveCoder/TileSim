using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class FoodPrioritizer
	{
		public FoodPrioritizer ()
		{
		}

		public void Prioritize(Person person)
		{
			PrioritizeCritical (person);

			PrioritizeSecondary (person);
		}

		public void PrioritizeCritical(Person person)
		{
			person.Priorities [PriorityTypes.Food] = person.Hunger;

			// TODO: Should this be here?
			if (person.Supplies[SupplyTypes.Food] > person.SuppliesMax[SupplyTypes.Food]/2)
				person.Priorities [PriorityTypes.Food] = person.Hunger/2;
		}

		public void PrioritizeSecondary(Person person)
		{
			if (!person.IsActive) {
				person.Priorities [PriorityTypes.Food] = 60;
			}
		}
	}
}

