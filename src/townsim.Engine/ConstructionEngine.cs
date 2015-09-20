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

		public EngineSettings Settings { get;set; }

		public ConstructionEngine (EngineSettings settings)
		{
			Settings = settings;
		}

		public void Update(Town town)
		{
			StartBuildHouses (town);

			HireWorkers (town);

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

		public void HireWorkers(Town town)
		{
			foreach (var house in town.Buildings.Houses) {
				if (!house.IsCompleted
					&& house.Workers.Length < 2)
					Workers.Hire (town, house);
			}
		}

		public int CalculateNumberOfNewHousesToStart(Town town)
		{
			var housesNeeded = town.TotalHomelessPeople;

			int numberOfHousesToBuild = 0;

			numberOfHousesToBuild = housesNeeded;

			numberOfHousesToBuild -= town.Buildings.TotalIncompleteHouses;

			numberOfHousesToBuild = GetConstructionLimit (town, numberOfHousesToBuild);
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

		public int GetConstructionLimit(Town town, int numberOfHousesToBuild)
		{
			var limit = numberOfHousesToBuild;

			if (town.Population > 5
			    && town.Population <= 10)
				limit = town.Population / 2;
			else if (town.Population > 10)
				limit = town.Population / 5;
			else if (town.Population > 100)
				limit = town.Population / 10;
			

			return ApplyLimit (numberOfHousesToBuild, limit);
				
		}

		public int ApplyLimit(int numberToLimit, int maximumValue)
		{
			if (numberToLimit > maximumValue)
				return maximumValue;
			else
				return numberToLimit;
		}

		public void StartBuildHouse(Town town)
		{
			if (town.TotalUnemployed > 0) {
				var house = new Building (Entities.BuildingType.House);

				Workers.Hire (town, house);

				if (house.Workers.Length > 0) {
					town.Buildings.Add (house);

					LogWriter.Current.AppendLine (CurrentEngine.Id, "A new house is under construction.");
				}
			}
		}

		public void DoConstruction(Town town)
		{
			foreach (var house in town.Buildings.Houses) {
				// Do the work
				if (!house.IsCompleted && house.Workers.Length > 0) {
					DoConstruction (town, house);
				}
				// Job done, fire the workers
				if (house.PercentComplete >= 100
					&& !house.IsCompleted) {

					LogWriter.Current.AppendLine (CurrentEngine.Id, "A house has been completed.");

					house.PercentComplete = 100;
					house.IsCompleted = true;
					Workers.Fire (town, house);
				}
			}
		}

		public void DoConstruction(Town town, Building building)
		{
			if (building.Workers.Length > 0) {
				if (building.TimberPending > 0) {
					if (Timber.IsTimberAvailable (town, building)) {
						Timber.MillTimber (town, building);
					}
				}

				var workDone = ConstructionRate * building.Workers.Length * Settings.GameSpeed;
				building.PercentComplete += workDone; 
			}
		}
	}
}

