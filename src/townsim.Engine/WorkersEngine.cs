using System;
using townsim.Data;
using townsim.Entities;
using System.Collections.Generic;
using System.Linq;
using datamanager.Entities;

namespace townsim.Engine
{
	public class WorkersEngine
	{
		public WorkersEngine ()
		{
		}

		public int Hire(Town town, int numberOfWorkersToHire, ActivityType employmentType, IActivityTarget target)
		{
			if (numberOfWorkersToHire > town.TotalInactive)
				return 0;
			else {
				//town.Workers += numberOfWorkersToHire;
				int numberOfWorkersHired = 0;
				foreach (var person in town.People) {
					if (person.CanWork && !person.IsActive
					    && numberOfWorkersHired < numberOfWorkersToHire) {
						Hire (town, person, employmentType, target);
						numberOfWorkersHired++;
					}
				}
				return numberOfWorkersHired;
			}
		}

		public void Hire(Town town, Person person, ActivityType employmentType, IActivityTarget target)
		{
			person.Activity = employmentType;
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

		/*public bool Fire(Town town, int numberOfWorkersToFire)
		{
			var available = town.TotalEmployed;

			if (numberOfWorkersToFire > available)
				numberOfWorkersToFire = available;

			int numberOfWorkersFired = 0;
			foreach (var person in town.People) {
				if (person.IsEmployed && numberOfWorkersFired < numberOfWorkersToFire)
					Fire (town, person);
			}
			//town.Workers -= numberOfWorkersToFire;
			return true;
		}*/

		public void Fire(Person person)
		{
			person.Activity = ActivityType.Inactive;

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

