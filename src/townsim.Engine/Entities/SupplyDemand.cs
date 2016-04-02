using System;
using townsim.Engine.Activities;
using townsim.Engine;
using townsim.Engine.Needs;

namespace townsim.Entities
{
	[Serializable]
	public class SupplyDemand
	{
		public Person Person { get; set; }

		public NeedType Supply { get; set; }

		public decimal Amount { get;set; }

		public BaseActivity Activity { get; set; }

		public BaseGameEntity Target { get; set; }

		public SupplyDemand (Person person, NeedType needType, decimal amount)
		{
			throw new NotImplementedException ();
			Person = person;
			Supply = needType;
			Amount = amount;
		}
	}
}

