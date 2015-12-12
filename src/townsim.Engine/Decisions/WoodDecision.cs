using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class WoodDecision : BaseDecision
	{
		public WoodDecision (EngineSettings settings) : base(settings)
		{
		}

		public override ActivityType Decide(Person person)
		{
			if (person.ActivityType != ActivityType.FellWood) {
				if (person.HasDemand (SupplyTypes.Wood)) {
					if (person.Town.Trees.Length > 0) {
						person.Start (ActivityType.FellWood);
					}
				}
			}

			return person.ActivityType;
		}
	}
}

