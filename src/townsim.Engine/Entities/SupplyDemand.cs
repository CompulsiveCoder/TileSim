using System;
using townsim.Engine.Activities;

namespace townsim.Entities
{
	[Serializable]
	public class SupplyDemand
	{
		public Person Person { get; set; }

		public SupplyTypes Supply { get; set; }

		public decimal Amount { get;set; }

		public BaseActivity Activity { get; set; }

		public BaseEntity Target { get; set; }

		public SupplyDemand (Person person, SupplyTypes supplyType, decimal amount)
		{
			Person = person;
			Supply = supplyType;
			Amount = amount;
		}
	}
}

