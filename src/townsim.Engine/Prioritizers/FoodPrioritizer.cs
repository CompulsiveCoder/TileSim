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
			person.Priorities [PriorityTypes.Food] = person.Hunger;
		}
	}
}

