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
			var availableWorkers = town.WorkersAvailable;
			var workersNeeded = WorkersPerBuilding;
			var workersToHire = 0;

			// If there's enough workers take as many as needed
			if (availableWorkers >= workersNeeded)
				workersToHire = workersNeeded;
			else // Otherwise take what's available
				workersToHire = availableWorkers;
			
			Workers.Hire (town, workersToHire);

			building.WorkerCount = workersToHire;
			town.Builders += workersToHire;
			// Don't increment town.Workers because that is done in Workers.Hire(...)

		}
		/*public void Hire(Town town, int buildersToHire)
		{
			var availableWorkers = town.WorkersAvailable;
			if (buildersToHire > availableWorkers)
				buildersToHire = availableWorkers;
			Workers.Hire (town, buildersToHire);
			town.Builders += buildersToHire;
			
		}*/

		public void Fire(Town town, Building building)
		{
			var workersToFire = building.WorkerCount;

			Workers.Fire (town, workersToFire);

			town.Builders -= workersToFire;
			// Don't decrement town.Workers because that is done in Workers.Hire(...)
		}
	}
}

