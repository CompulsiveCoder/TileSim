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

		public bool Hire(Town town, int numberOfWorkersToHire, EmploymentType employmentType)
		{
			if (numberOfWorkersToHire > town.TotalUnemployed)
				return false;
			else {
				//town.Workers += numberOfWorkersToHire;
				int numberOfWorkersHired = 0;
				foreach (var person in town.People) {
					if (person.CanWork && !person.IsEmployed
					    && numberOfWorkersHired < numberOfWorkersToHire) {
						Hire (town, person, employmentType);
						numberOfWorkersHired++;
					}
				}
				return true;
			}
		}

		public void Hire(Town town, Person person, EmploymentType employmentType)
		{
			person.IsEmployed = true;
			person.Employment = employmentType;
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
			person.Employment = EmploymentType.NotSet;
		}
	}
}
