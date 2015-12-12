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

		public Building Building
		{
			get { return (Building)Target; } 
			set { Target = value; }
		}

		public BuildActivity (Person person, EngineSettings settings, EngineClock clock) : base(person, settings, clock)
		{
		}

		public override void ExecuteSingleCycle()
		{
			if (Person.Home == null && Person.ActivityType == ActivityType.Builder) {
				StartBuildingAHouse ();
			}

			if (Person.ActivityType == ActivityType.Builder) {
				PerformConstructionCycle ();
			}
		}

		public void StartBuildingAHouse()
		{
			var house = new Building (BuildingType.House);
			house.ConstructionStartTime = Clock.GameDuration;

			house.AddLink ("People", Person);

			SetTarget (house);

			if (Person.Town == null)
				throw new Exception ("The person.Town property cannot be null.");
			
			Person.Town.Buildings.Add (house);

			EnsureSupplies (Person, house);

			if (Person.Home.Id == house.Id && Person.Id == CurrentEngine.PlayerId && Settings.OutputType == ConsoleOutputType.General)
				LogWriter.Current.AppendLine (CurrentEngine.Id, "The player has started building a home.");
			else
				LogWriter.Current.AppendLine (CurrentEngine.Id, "A new house is under construction.");
		}

		public void EnsureSupplies(Person person, Building house)
		{
			if (person.Supplies [SupplyTypes.Timber] < house.TimberPending) {
				if (Settings.OutputType == ConsoleOutputType.General) {
					Console.WriteLine ("Not enough timber. There is a demand for timber.");
				}
				person.Demands.Add (new SupplyDemand (person, SupplyTypes.Timber, house.TimberPending));
			}
		}

		public void PerformConstructionCycle()
		{
			var house = Person.Home;

			var town = Person.Town;

			if (BuildingNeedsTimber(house)) {
				if (Person.Has (SupplyTypes.Timber, house.TimberPending)) {
					MoveTimberFromPersonToBuilding (Person, house);
				} else {
					Person.AddDemand (SupplyTypes.Timber, house.TimberPending);
					Person.Start (ActivityType.Inactive);
				}
			}
			else
			{
				// Do the work
				if (!IsComplete()) {
					ApplyConstructionCycleNumbers (Person);
				}
				else
				{
					house.PercentComplete = 100;
					house.IsCompleted = true;
					house.ConstructionEndTime = Clock.GameDuration;

					Finish ();

					if (Person.Home.Id == house.Id && Person.Id == CurrentEngine.PlayerId)
						LogWriter.Current.AppendLine (CurrentEngine.Id, "The player completed their house. Duration: " + Clock.GetTimeSpanString (house.ConstructionDuration));
					else
						LogWriter.Current.AppendLine (CurrentEngine.Id, "A house has been completed. Duration: " + Clock.GetTimeSpanString (house.ConstructionDuration));
				}
			}
		
		}

		public bool BuildingNeedsTimber(Building house)
		{
			var value = house.TimberPending > 0;

			return value;
		}

		public bool BuildingIsFinished(Building building)
		{
			var value = building.PercentComplete >= 100;
				//&& !building.IsCompleted; // TODO: Remove if not needed

			return value;
		}

		public void ApplyConstructionCycleNumbers(Person person)//, Building building)
		{
			//var town = person.Town;

			//if (building.People.Length > 0) {
				//if (building.TimberPending > 0) {
				//	if (Timber.IsTimberAvailable (town)) {
				//		Timber.MillTimber (person);
				//	}
				//}

			var building = (Building)person.ActivityTarget;
			var workDone = ConstructionRate;
			building.PercentComplete += workDone; 
			//}
		}

		public void MoveTimberFromPersonToBuilding(Person person, Building building)
		{
			if (Settings.OutputType == ConsoleOutputType.General
			    && CurrentEngine.PlayerId == person.Id) {
				Console.WriteLine ("Transferring " + building.TimberPending + " timber from person to building.");
			}
			person.Supplies [SupplyTypes.Timber] = person.Supplies [SupplyTypes.Timber] - building.TimberPending;
			building.Timber += building.TimberPending;
		}

		public override void Start ()
		{
			Person.Start (ActivityType.Builder);

			if (Person.Home == null && Person.ActivityType == ActivityType.Builder) {
				StartBuildingAHouse ();
			}
		}

		public override bool IsComplete ()
		{
			var building = ((Building)Person.ActivityTarget);

			var isComplete = building != null
				&& (building.PercentComplete >= 100
					|| building.IsCompleted);

			return isComplete;
		}

		public override bool IsImpossible ()
		{
			return Building != null
				&& Building.TimberPending > Person.Supplies [SupplyTypes.Timber];
		}

		public override void Finish ()
		{
			if (Building.PercentComplete > 100)
				Building.PercentComplete = 100;
			
			CleanUp ();
		}
	}
}

