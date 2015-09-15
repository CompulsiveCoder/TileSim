using System;
using townsim.Data;
using townsim.Entities;

namespace townsim.Engine
{
	public class ConstructionWorkersEngine
	{
		public int WorkersPerBuilding = 2;

		WorkersEngine Workers = new WorkersEngine ();

		public ConstructionWorkersEngine ()
		{
		}

		public void Hire(Town town, Building building)
		{
			var availableWorkers = town.TotalUnemployed;
			var workersNeeded = WorkersPerBuilding;
			var workersToHire = 0;

			// If there's enough workers take as many as needed
			if (availableWorkers >= workersNeeded)
				workersToHire = workersNeeded;
			else // Otherwise take what's available
				workersToHire = availableWorkers;
			
			Workers.Hire (town, workersToHire, EmploymentType.Builder, building);

			//building.Workers = town.GetWorkers (2);

			//building.WorkerCount = workersToHire;
		}

		public void Fire(Town town, Building building)
		{
			Workers.Fire (building);

			//building.Workers = new Person[]{ };
		}
	}
}

