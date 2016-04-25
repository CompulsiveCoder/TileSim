using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine
{
	public class ShelterPrioritizer
	{
		public ShelterPrioritizer ()
		{
		}

		public void Prioritize(Person person)
		{
			throw new NotImplementedException ();

			/*if (person.IsHomeless) {
				person.Needs [NeedType.Shelter] = 100;
			} else {
				person.Needs [NeedType.Shelter] = 0;
			}*/
		}
	}
}

