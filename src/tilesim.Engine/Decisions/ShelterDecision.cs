using System;
using tilesim.Engine.Entities;
using tilesim.Engine.Decisions;
using tilesim.Engine.Activities;

namespace tilesim.Engine
{
	public class ShelterDecision : BaseDecision
	{
		public ShelterDecision (EngineSettings settings) : base(settings)
		{
		}

		public override void Decide(Person person)
		{
			if (person.IsHomeless)
			{
				throw new NotImplementedException ();

				//var buildShelterActivity = new BuildShelterActivity (person, needEntry, Settings);
				//person.AddActivity (buildShelterActivity);

				// TODO: Clean up
				/*

				var amountOfTimberRequired = buildShelterActivity.TimberCost;

				var millTimberActivity = new BuildShelterActivity (person, Settings);
				millTimberActivity.SetQuantity (amountOfTimberRequired);

				var amountOfWoodRequired = amountOfTimberRequired * Settings.WoodRequiredForTimber;

				var fellWoodActivity = new BuildShelterActivity (person, Settings);
				fellWoodActivity.SetQuantity (amountOfWoodRequired);

				person.AddActivity (fellWoodActivity);
				person.AddActivity (millTimberActivity);*/
				
				// TODO: Remove if not needed
				// If house hasn't been started, start it
				/*if (person.Home == null)
					person.Assign (ActivityType.Builder);
				// If house has been started and there's enough timber, build it
				else if (person.Home != null
				         && (person.Has (NeedType.Timber, person.Home.TimberPending)
				         || person.Home.TimberPending == 0)) {
					person.Assign (ActivityType.Builder);
				}*/
				//else if (person.Home.TimberPending > 0) {
				//	person.AddDemand (NeedType.Timber, person.Home.TimberPending);
				//}
				//else if (person.Supplies [NeedType.Timber] >= person.Home.TimberPending) {
				//	var amount = person.Home.TimberPending;
				//	person.Home.Timber += amount;
				//	person.Supplies [NeedType.Timber] = person.Supplies [NeedType.Timber] - amount;
				//}
				//else
				//	new TimberDecision (Settings).Decide (person);
			}

			//return person.Activity.Type;
		}
	}
}

