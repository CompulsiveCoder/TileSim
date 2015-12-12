using System;
using townsim.Entities;
using townsim.Engine.Decisions;

namespace townsim.Engine
{
	public class ShelterDecision : BaseDecision
	{
		public ShelterDecision (EngineSettings settings) : base(settings)
		{
		}

		public override ActivityType Decide(Person person)
		{
			if (person.IsHomeless)
			{
				// If house hasn't been started, start it
				if (person.Home == null)
					person.Start (ActivityType.Builder);
				// If house has been started and there's enough timber, build it
				else if (person.Home != null
				         && (person.Has (SupplyTypes.Timber, person.Home.TimberPending)
				         || person.Home.TimberPending == 0)) {
					person.Start (ActivityType.Builder);
				}
				//else if (person.Home.TimberPending > 0) {
				//	person.AddDemand (SupplyTypes.Timber, person.Home.TimberPending);
				//}
				/*else if (person.Supplies [SupplyTypes.Timber] >= person.Home.TimberPending) {
					var amount = person.Home.TimberPending;
					person.Home.Timber += amount;
					person.Supplies [SupplyTypes.Timber] = person.Supplies [SupplyTypes.Timber] - amount;
				}
				else
					new TimberDecision (Settings).Decide (person);*/
			}

			return person.ActivityType;
		}
	}
}

