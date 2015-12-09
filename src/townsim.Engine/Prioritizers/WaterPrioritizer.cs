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
			person.Priorities [PriorityTypes.Water] = person.Thirst;
		}
	}
}

