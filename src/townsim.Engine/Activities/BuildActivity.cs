using System;
using townsim.Data;
using townsim.Entities;

namespace townsim.Engine.Activities
{
	public class BuildActivity
	{
		public double ConstructionRate = 0.2;

		public int ConstructionCountLimitBase = 3;
		public double ConstructionCountLimit = 0.1;

		public ConstructionWorkersUtility Workers = new ConstructionWorkersUtility();
		public WoodChopActivity Timber = new WoodChopActivity ();

		public EngineSettings Settings { get;set; }

		public EngineClock Clock { get;set; }

		public BuildActivity (EngineSettings settings, EngineClock clock)
		{
			Settings = settings;
			Clock = clock;
		}

		public void Update(Person person)
		{
			if (person.Home == null && person.Activity == ActivityType.Builder) {
				StartBuildingAHouse (person);
			}

			if (person.Activity == ActivityType.Builder) {
				DoConstruction (person);
			}
		}

		/// <summary>
		/// Loops through all the people in the town to call the Update(person) function
		/// </summary>
		/// <param name="town">Town.</param>
		public void Update(Town town)
		{
			foreach (var person in town.People) {
				Update (person);
			}
		}

		public void StartBuildingAHouse(Person person)
		{
			var house = new Building (Entities.BuildingType.House);
			house.ConstructionStartTime = Clock.GameDuration;

			house.AddLink ("People", person);

			person.Town.Buildings.Add (house);

			LogWriter.Current.AppendLine (CurrentEngine.Id, "A new house is under construction.");
		}


		//public void StartBuildHouses(Town town)
		//{
		//	foreach (var persion in town.People
			//var numberOfHousesToBuild = CalculateNumberOfNewHousesToStart (town);
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
			//for (int i = 0; i < numberOfHousesToBuild; i++) {
			//		StartBuildHouse (town);
			//	}
			//}
		//}

		/*public void HireWorkers(Town town)
		{
			foreach (var house in town.Buildings.Houses) {
				if (!house.IsCompleted
				    && house.People.Length < 2) {

					Workers.Hire (town, house);

					// The approach has changed. Instead of hiring workers, each person just builds their own house
					house.People [0].Home = house.GetLink();
				}
			}
		}*/

		//public int CalculateNumberOfNewHousesToStart(Town town)
		//{
		//	var housesNeeded = town.TotalHomelessPeople;

		//	int numberOfHousesToBuild = 0;

		//	numberOfHousesToBuild = housesNeeded;

		//	numberOfHousesToBuild -= town.Buildings.TotalIncompleteHouses;

		///	numberOfHousesToBuild = GetConstructionLimit (town, numberOfHousesToBuild);
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

		//	return numberOfHousesToBuild;
		//}

		/*public int GetConstructionLimit(Town town, int numberOfHousesToBuild)
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
		}*/

		public void StartBuildHouse(Town town)
		{
			if (town.TotalInactive > 0) {
				var house = new Building (Entities.BuildingType.House);
				house.ConstructionStartTime = Clock.GameDuration;

				Workers.Hire (town, house);

				if (house.People.Length > 0) {
					town.Buildings.Add (house);

					LogWriter.Current.AppendLine (CurrentEngine.Id, "A new house is under construction.");
				}
			}
		}

		public void DoConstruction(Person person)
		{
			var house = person.Home;

			var town = person.Town;

			// Do the work
			if (!house.IsCompleted && house.People.Length > 0) {
				DoConstructionNumbers (person, house);
			}
			// Job done, fire the workers
			if (house.PercentComplete >= 100
			     && !house.IsCompleted) {

				house.PercentComplete = 100;
				house.IsCompleted = true;
				house.ConstructionEndTime = Clock.GameDuration;

				Workers.Fire (town, house);

				LogWriter.Current.AppendLine (CurrentEngine.Id, "A house has been completed. Duration: " + Clock.GetTimeSpanString (house.ConstructionDuration));
			}
		
		}

		public void DoConstructionNumbers(Person person, Building building)
		{
			var town = person.Town;

			if (building.People.Length > 0) {
				if (building.TimberPending > 0) {
					if (Timber.IsTimberAvailable (town, building)) {
						Timber.MillTimber (town, building);
					}
				}

				var workDone = ConstructionRate * building.People.Length * Settings.GameSpeed;
				building.PercentComplete += workDone; 
			}
		}
	}
}

