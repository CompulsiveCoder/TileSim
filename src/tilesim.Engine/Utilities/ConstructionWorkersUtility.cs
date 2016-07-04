using System;
using tilesim.Data;
using tilesim.Entities;

namespace tilesim.Engine.Activities
{
	[Serializable]
	public class ConstructionWorkersUtility
	{
		public int WorkersPerBuilding = 2;

		WorkersUtility Workers = new WorkersUtility ();

		public ConstructionWorkersUtility ()
		{
		}

		public void Hire(Tile tile, Building building)
		{
			var availableWorkers = tile.TotalInactive;
			var workersNeeded = WorkersPerBuilding;
			var workersToHire = 0;

			// If there's enough workers take as many as needed
			if (availableWorkers >= workersNeeded)
				workersToHire = workersNeeded;
			else // Otherwise take what's available
				workersToHire = availableWorkers;
			
			Workers.Hire (tile, workersToHire, ActivityType.Builder, building);

			//building.Workers = tile.GetWorkers (2);

			//building.WorkerCount = workersToHire;
		}

		public void Fire(Tile tile, Building building)
		{
			Workers.Fire (building);

			//building.Workers = new Person[]{ };
		}
	}
}

