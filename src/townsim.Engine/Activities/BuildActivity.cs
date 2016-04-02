using System;
using townsim.Data;
using townsim.Entities;

namespace townsim.Engine.Activities
{
	[Serializable]
	public class BuildActivity : BaseActivityOld
	{
		public Building Building
		{
			get { return (Building)Target; } 
			set { Target = value; }
		}

		public BuildActivity (Person person, EngineContext context)
			: base(ActivityType.Builder, person, context)
		{
		}

		protected override void ExecuteSingleCycle()
		{
			if (Person.Home == null)
				StartBuildingAHouse ();
			else
				PerformConstructionCycle ();
		}

		public void StartBuildingAHouse()
		{
			var house = new Building (BuildingType.House);

			house.ConstructionStartTime = Context.Clock.GameDuration;

			house.AddLink ("People", Person);

			SetTarget (house);

			if (Person.Town == null)
				throw new Exception ("The person.Town property cannot be null.");
			
			Person.Town.AddLink("Buildings", house);

			EnsureSupplies (Person, house);

			if (Context.Settings.OutputType == ConsoleOutputType.Debug) {
				if (Person.Home.Id == house.Id && Person.Id == Context.Settings.PlayerId)
					Context.Log.WriteLine ("The player has started building a home.");
				else
					Context.Log.WriteLine ("A new house is under construction.");
			}
		}

		public void EnsureSupplies(Person person, Building house)
		{
			if (person.Supplies [SupplyTypes.Timber] < house.TimberPending) {

				if (Context.Settings.OutputType == ConsoleOutputType.Debug) {
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
					Cancel ();
				}
			}
			else
				IncreasePercentComplete ();
		
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

		public void IncreasePercentComplete()
		{
			var building = (Building)Person.Activity.Target;
			var workDone = Context.Settings.ConstructionRate;
			building.PercentComplete += workDone; 
		}

		public void MoveTimberFromPersonToBuilding(Person person, Building building)
		{
			if (Context.Settings.OutputType == ConsoleOutputType.Debug
				&& Context.Settings.PlayerId == person.Id) {
				Console.WriteLine ("Transferring " + building.TimberPending + " timber from person to building.");
			}
			person.Supplies [SupplyTypes.Timber] = person.Supplies [SupplyTypes.Timber] - building.TimberPending;
			building.Timber += building.TimberPending;
		}

		public override void Start ()
		{
			//Person.Start (ActivityType.Builder);

			if (Person.Home == null && Person.ActivityType == ActivityType.Builder) {
				StartBuildingAHouse ();
			}
		}

		public override bool CheckComplete ()
		{
			if (Person.Activity == null)
				System.Diagnostics.Debugger.Break ();

			var building = ((Building)Person.Activity.Target);

			var isComplete = building != null
				&& (building.PercentComplete >= 100
					|| building.IsCompleted);

			return isComplete;
		}

		public override bool CheckImpossible ()
		{
			return Building != null
				&& Building.TimberPending > Person.Supplies [SupplyTypes.Timber];
		}

		public override void Finish ()
		{			
			Building.PercentComplete = 100;
			Building.IsCompleted = true;

			Building.ConstructionEndTime = Context.Clock.GameDuration;

			if (Person.Home.Id == Building.Id && Person.Id == Context.Settings.PlayerId)
				Context.Log.WriteLine ("The player completed their house. Duration: " + Context.Clock.GetTimeSpanString (Building.ConstructionDuration));
			else
				Context.Log.WriteLine ("A house has been completed. Duration: " + Context.Clock.GetTimeSpanString (Building.ConstructionDuration));
		}
	}
}

