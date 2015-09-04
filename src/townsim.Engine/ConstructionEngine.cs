using System;
using townsim.Data;
using townsim.Entities;

namespace townsim.Engine
{
	public class ConstructionEngine
	{
		public int ConstructionRate = 1;

		public int ConstructionCountLimitBase = 3;
		public double ConstructionCountLimit = 0.1;


		public ConstructionWorkersEngine Workers = new ConstructionWorkersEngine();
		public TimberEngine Timber = new TimberEngine ();

		public ConstructionEngine ()
		{
		}

		public void Update(Town town)
		{
			StartBuildHouses (town);

			DoConstruction (town);
		}

		public void StartBuildHouses(Town town)
		{
			var numberOfHousesToBuild = CalculateNumberOfNewHousesToStart (town);
				/*var fractionToBuild = 0;
				if (town.Population > 100)
					fractionToBuild = housesNeeded / 10;
				else
					fractionToBuild = housesNeeded;

				// Count the number of houses already under construction
				var incompleteHouses = town.Persons.TotalIncompleteHouses;

				// Exclude houses already under construction
				fractionToBuild -= incompleteHouses;
*/
			for (int i = 0; i < numberOfHousesToBuild; i++) {
					StartBuildHouse (town);
				}
			//}
		}

		public int CalculateNumberOfNewHousesToStart(Town town)
		{
			var housesNeeded = town.TotalHomelessPeople;

			int numberOfHousesToBuild = 0;

			numberOfHousesToBuild = housesNeeded;

			numberOfHousesToBuild -= town.Buildings.TotalIncompleteHouses;

			numberOfHousesToBuild = LimitConstructionNumber (town, numberOfHousesToBuild);
			// Limit the number of houses being built
			//if (town.Persons.TotalIncompleteHouses <= housesNeeded) {
				//if (town.Population > ConstructionCountLimitBase) {
			//		numberOfHousesToBuild = (int)((double)housesNeeded * ConstructionCountLimit);
				//} else {
				//	numberOfHousesToBuild = housesNeeded;
				//}
			//}//

			//numberOfHousesToBuild -= town.Persons.TotalIncompleteHouses;

			// Limit to the number of workers available
			//if (numberOfHousesToBuild > town.WorkersAvailable / Workers.WorkersPerPerson)
			//	numberOfHousesToBuild = town.WorkersAvailable / Workers.WorkersPerPerson;

			return numberOfHousesToBuild;
		}

		public int LimitConstructionNumber(Town town, int numberOfHousesToBuild)
		{
			var limit = 1;
			if (town.Population > 5
			    && town.Population <= 10)
				limit = town.Population / 5;
			else if (town.Population > 10) {
				limit = town.Population / 10;
			}

			return ApplyLimit (numberOfHousesToBuild, limit);
				
		}

		public int ApplyLimit(int limitedNumber, int maximumValue)
		{
			if (limitedNumber < maximumValue)
				return limitedNumber;
			else
				return maximumValue;
		}

		public void StartBuildHouse(Town town)
		{
			if (town.TotalUnemployed > 0) {
				var house = new Building (Entities.BuildingType.House);
				town.Buildings.Add (house);
				Workers.Hire (town, house);
			}
		}

		public void DoConstruction(Town town)
		{
			foreach (var house in town.Buildings.Houses) {
				// Hire workers
				//if (house.PercentComplete == 0 && house.WorkerCount == 0) {
				//	Workers.Hire (town, house);
				//	house.PercentComplete = 1; // Indicates work has started
				//}
				// Do the work
				if (!house.IsCompleted && house.WorkerCount > 0) {
					DoConstruction (town, house);
				}
				// Job done, fire the workers
				if (house.PercentComplete >= 100
					&& !house.IsCompleted) {
					house.PercentComplete = 100;
					house.IsCompleted = true;
					Workers.Fire (town, house);
				}
			}
		}

		public void DoConstruction(Town town, Building building)
		{
			if (Timber.IsTimberAvailable (town, building)) {
				Timber.RefineTimber (town, building);
				var workDone = ConstructionRate * town.TotalEmployed;
				building.PercentComplete += workDone; 
			}
		}


		/*public void HireBuilders(Town town, int buildersToHire)
		{
			var availableWorkers = town.WorkersAvailable;
			if (buildersToHire > availableWorkers)
				buildersToHire = availableWorkers;
			Workers.Hire (town, buildersToHire);
			town.Builders += buildersToHire;

		}

		public void FireBuilders(Town town, Building building)
		{
			//var workersToFire = building.Workers;
			building.WorkerCount = 0;
			Workers.Fire (town, building.WorkerCount);
			//town.Builders -= workersToFire;
		}*/
	}
}

