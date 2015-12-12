using System;
using NUnit.Framework;
using townsim.Entities;

namespace townsim.Engine.Tests.Unit.Entities
{
	[TestFixture]
	public class PersonTestFixture
	{
		[Test]
		public void Test_RemoveDemand_SameAmount()
		{
			var person = new Person ();

			person.AddDemand (SupplyTypes.Timber, 50);
			person.AddDemand (SupplyTypes.Timber, 25);
			person.AddDemand (SupplyTypes.Timber, 25);

			person.RemoveDemand (SupplyTypes.Timber, 100);

			Assert.AreEqual (0, person.GetDemandAmount (SupplyTypes.Timber));
		}

		[Test]
		public void Test_RemoveDemand_DemandLeftOver()
		{
			var person = new Person ();

			person.AddDemand (SupplyTypes.Timber, 50);
			person.AddDemand (SupplyTypes.Timber, 25);

			person.RemoveDemand (SupplyTypes.Timber, 50);

			Assert.AreEqual (25, person.GetDemandAmount (SupplyTypes.Timber));
		}
	}
}

