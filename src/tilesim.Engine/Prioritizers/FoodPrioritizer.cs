using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine
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
			throw new NotImplementedException ();
			/*
			person.Priorities [PriorityTypes.Food] = person.Hunger;

			// TODO: Should this be here?
			if (person.Supplies[needTypes.Food] > person.SuppliesMax[needTypes.Food]/2)
				person.Priorities [PriorityTypes.Food] = person.Hunger/2;
*/
		}

		public void PrioritizeSecondary(Person person)
		{

			throw new NotImplementedException ();
			/*
			if (!person.IsActive) {
				person.Priorities [PriorityTypes.Food] = 60;
			}
			*/
		}
	}
}

