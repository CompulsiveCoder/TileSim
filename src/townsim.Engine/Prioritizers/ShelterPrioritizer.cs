using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class ShelterPrioritizer
	{
		public ShelterPrioritizer ()
		{
		}

		public void Prioritize(Person person)
		{
			if (person.IsHomeless) {
				person.Priorities [PriorityTypes.Shelter] = 100;
			} else {
				person.Priorities [PriorityTypes.Shelter] = 0;
			}
		}
	}
}

