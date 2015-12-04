using System;
using townsim.Data;
using townsim.Entities;
using System.Collections.Generic;

namespace townsim.Engine
{
	public class WorkersEngine
	{
		public WorkersEngine ()
		{
		}

		public int Hire(Town town, int numberOfWorkersToHire, ActivityType employmentType, IEmploymentTarget target)
		{
			if (numberOfWorkersToHire > town.TotalUnemployed)
				return 0;
			else {
				//town.Workers += numberOfWorkersToHire;
				int numberOfWorkersHired = 0;
				foreach (var person in town.People) {
					if (person.CanWork && !person.IsEmployed
					    && numberOfWorkersHired < numberOfWorkersToHire) {
						Hire (town, person, employmentType, target);
						numberOfWorkersHired++;
					}
				}
				return numberOfWorkersHired;
			}
		}

		public void Hire(Town town, Person person, ActivityType employmentType, IEmploymentTarget target)
		{
			person.IsEmployed = true;
			person.Activity = employmentType;
			person.EmploymentTarget = target;
		
			AddWorkerToTarget (target, person);
		}

		public void AddWorkerToTarget(IEmploymentTarget target, Person person)
		{
			var workers = new List<Person> (target.Workers);
			workers.Add (person);
			target.Workers = workers.ToArray ();
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
			person.IsEmployed = false;
			person.Activity = ActivityType.Inactive;

			if (person.EmploymentTarget != null) {
				person.EmploymentTarget.Workers = new Person[]{ };
				person.EmploymentTarget = null;
			}
		}

		public void Fire(IEmploymentTarget target)
		{
			foreach (var person in target.Workers) {
				Fire (person);
			}

			if (target.Workers.Length > 0) {
				target.Workers = new Person[]{ };
			}
		}
	}
}

