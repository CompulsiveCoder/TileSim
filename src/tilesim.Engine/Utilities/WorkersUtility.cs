using System;
using tilesim.Data;
using tilesim.Entities;
using System.Collections.Generic;
using System.Linq;
using datamanager.Entities;

namespace tilesim.Engine
{
	[Serializable]
	public class WorkersUtility
	{
		public WorkersUtility ()
		{
		}

		public int Hire(Tile tile, int numberOfWorkersToHire, ActivityType employmentType, IActivityTarget target)
		{
			if (numberOfWorkersToHire > tile.TotalInactive)
				return 0;
			else {
				//tile.Workers += numberOfWorkersToHire;
				int numberOfWorkersHired = 0;
				foreach (var person in tile.People) {
					if (person.CanWork && !person.IsActive
					    && numberOfWorkersHired < numberOfWorkersToHire) {
						Hire (tile, person, employmentType, target);
						numberOfWorkersHired++;
					}
				}
				return numberOfWorkersHired;
			}
		}

		public void Hire(Tile tile, Person person, ActivityType employmentType, IActivityTarget target)
		{
			person.ActivityType = employmentType;
			person.ActivityTarget = target;
		
			AddWorkerToTarget (target, person);
		}

		public void AddWorkerToTarget(IActivityTarget target, Person person)
		{
			var people = new List<Person> ();

			people.AddRange (
				(from p in target.People
				where p.Id == person.Id
					select p
				).ToArray()
			);
			people.Add (person);
			target.People = people.ToArray ();
		}

		/*public bool Fire(Tile tile, int numberOfWorkersToFire)
		{
			var available = tile.TotalEmployed;

			if (numberOfWorkersToFire > available)
				numberOfWorkersToFire = available;

			int numberOfWorkersFired = 0;
			foreach (var person in tile.People) {
				if (person.IsEmployed && numberOfWorkersFired < numberOfWorkersToFire)
					Fire (tile, person);
			}
			//tile.Workers -= numberOfWorkersToFire;
			return true;
		}*/

		public void Fire(Person person)
		{
			person.ActivityType = ActivityType.Inactive;

			if (person.ActivityTarget != null) {
				person.ActivityTarget.People = new Person[]{ };
				person.ActivityTarget = null;
			}
		}

		public void Fire(IActivityTarget target)
		{
			foreach (var person in target.People) {
				Fire (person);
			}

			if (target.People.Length > 0) {
				target.People = new Person[]{ };
			}
		}
	}
}

