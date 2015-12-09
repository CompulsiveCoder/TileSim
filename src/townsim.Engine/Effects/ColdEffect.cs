using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class ColdEffect
	{
		public ColdEffect ()
		{
		}

		public void Update(Person person)
		{
			// TODO: Implement a complete temperature feature
			// The following is just placeholder code

			if (person.IsHomeless) {
				person.Priorities [PriorityTypes.Shelter] = 100;
				person.ActivityType = ActivityType.Builder;
			} else {
				person.Priorities [PriorityTypes.Shelter] = 0;
				person.ActivityType = ActivityType.Inactive;
			}

		}
	}
}

