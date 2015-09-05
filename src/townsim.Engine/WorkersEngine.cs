﻿using System;
using townsim.Data;
using townsim.Entities;

namespace townsim.Engine
{
	public class WorkersEngine
	{
		public WorkersEngine ()
		{
		}

		public bool Hire(Town town, int numberOfWorkersToHire, EmploymentType employmentType, IEmploymentTarget target)
		{
			if (numberOfWorkersToHire > town.TotalUnemployed)
				return false;
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
				return true;
			}
		}

		public void Hire(Town town, Person person, EmploymentType employmentType, IEmploymentTarget target)
		{
			person.IsEmployed = true;
			person.EmploymentType = employmentType;
			person.EmploymentTarget = target;
		}

		public bool Fire(Town town, int numberOfWorkersToFire)
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
		}

		public void Fire(Town town, Person person)
		{
			person.IsEmployed = false;
			person.EmploymentType = EmploymentType.NotSet;
			person.EmploymentTarget.Workers = new Person[]{ };
			person.EmploymentTarget = null;
		}
	}
}

