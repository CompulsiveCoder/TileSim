using System;
using townsim.Data;
using townsim.Entities;

namespace townsim.Engine
{
	public class WorkersEngine
	{
		public WorkersEngine ()
		{
		}

		public bool Hire(Town town, int numberOfWorkersToHire)
		{
			if (numberOfWorkersToHire > town.WorkersAvailable)
				return false;
			else {
				town.Workers += numberOfWorkersToHire;
				return true;
			}
		}

		public bool Fire(Town town, int numberOfWorkersToFire)
		{
			var available = town.Workers;

			if (numberOfWorkersToFire > available)
				numberOfWorkersToFire = available;
			
			town.Workers -= numberOfWorkersToFire;
			return true;
		}
	}
}

