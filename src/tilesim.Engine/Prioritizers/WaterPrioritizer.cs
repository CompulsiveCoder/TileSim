using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine
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
			throw new NotImplementedException ();
			/*person.Priorities [PriorityTypes.Water] = person.Thirst;

			// TODO: Should this be here?
			if (person.Supplies[needTypes.Water] > person.SuppliesMax[needTypes.Water]/2)
				person.Priorities [PriorityTypes.Water] = person.Thirst/2;*/
		}

		public void PrioritizeSecondary(Person person)
		{
			throw new NotImplementedException ();
			/*if (!person.IsActive) {
				person.Priorities [PriorityTypes.Water] = 60;
			}*/
		}

	}
}

