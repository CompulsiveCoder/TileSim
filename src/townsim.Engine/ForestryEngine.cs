using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class ForestryEngine
	{
		public double PlantingTimeCost = 60; // Seconds
		
		public ForestryEngine ()
		{
		}

		public void Update(Town town)
		{
			var treesToPlant = town.TreesToPlantPerDay;

			var workersNeeded = treesToPlant;

			//var plantsPerWorker = 10;

			//if (treesToPlant > plantsPerWorker)
			//	workersNeeded = 

			//var workersEngine = new WorkersEngine ();
			//workersEngine.Hire (town, workersNeeded, EmploymentType.Forestry, null);


		}
	}
}

