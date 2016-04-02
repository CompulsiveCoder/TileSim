using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class WoodDecision : BaseDecision
	{
		public WoodDecision (EngineContext context) : base(context)
		{
		}

		public override void Decide(Person person)
		{
			throw new NotImplementedException ();
			/*if (person.ActivityType != ActivityType.FellWood) {
				if (person.HasDemand (SupplyTypes.Wood)) {
					if (person.Town.Trees.Length > 0) {
						person.Assign (ActivityType.FellWood);
					}
				}
			}

			return person.ActivityType;*/
		}
	}
}

