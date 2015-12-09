using System;
using townsim.Data;
using townsim.Entities;

namespace townsim.Engine.Activities
{
	[Serializable]
	public class BuildActivity : BaseActivity
	{
		public double ConstructionRate = 0.2;

		public int ConstructionCountLimitBase = 3;
		public double ConstructionCountLimit = 0.1;

		public ConstructionWorkersUtility Workers = new ConstructionWorkersUtility();
		public ChopTimberActivity Timber = new ChopTimberActivity ();

		public BuildActivity (EngineSettings settings, EngineClock clock) : base(settings, clock)
		{
			Settings = settings;
			Clock = clock;
		}

		public override void Act()
		{
			if (Person.Home == null && Person.ActivityType == ActivityType.Builder) {
				StartBuildingAHouse (Person);
			}

			if (Person.ActivityType == ActivityType.Builder) {
				DoConstruction (Person);
			}
		}

		/*/// <summary>
		/// Loops through all the people in the town to call the Update(person) function
		/// </summary>
		/// <param name="town">Town.</param>
		public void Update(Town town)
		{
			foreach (var person in town.People) {
				Update (person);
			}
		}*/

		public void StartBuildingAHouse(Person person)
		{
			var house = new Building (Entities.BuildingType.House);
			house.ConstructionStartTime = Clock.GameDuration;

			house.AddLink ("People", person);

			person.Town.Buildings.Add (house);

			if (person.Home.Id == house.Id && person.Id == CurrentEngine.PlayerId)
				LogWriter.Current.AppendLine (CurrentEngine.Id, "The player has started building a home.");
			else
				LogWriter.Current.AppendLine (CurrentEngine.Id, "A new house is under construction.");
		}

		/*public void StartBuildHouse(Town town)
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
		}*/

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

				person.FinishActivity ();

				if (person.Home.Id == house.Id && person.Id == CurrentEngine.PlayerId)
					LogWriter.Current.AppendLine (CurrentEngine.Id, "The player completed their house. Duration: " + Clock.GetTimeSpanString (house.ConstructionDuration));
				else
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

