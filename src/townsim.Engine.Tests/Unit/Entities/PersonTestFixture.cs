using System;
using NUnit.Framework;
using townsim.Entities;
using townsim.Engine.Needs;

namespace townsim.Engine.Tests.Unit.Entities
{
	// TODO: Remove if not needed
	//[TestFixture]
	public class PersonTestFixture
	{
		// TODO: Remove if not needed
		//[Test]
		public void Test_RemoveDemand_SameAmount_SingleDemand()
		{
			var person = new Person ();

			person.AddDemand (NeedType.Timber, 100);

			person.RemoveDemand (NeedType.Timber, 100);

			var remainingAmount = person.GetDemandAmount (NeedType.Timber);

			Assert.AreEqual (0, remainingAmount);
		}

		// TODO: Remove if not needed
		//[Test]
		public void Test_RemoveDemand_SameAmount_MultipleDemands()
		{
			var person = new Person ();

			person.AddDemand (NeedType.Timber, 50);
			person.AddDemand (NeedType.Timber, 25);
			person.AddDemand (NeedType.Timber, 25);

			person.RemoveDemand (NeedType.Timber, 100);

			var remainingAmount = person.GetDemandAmount (NeedType.Timber);

			Assert.AreEqual (0, remainingAmount);
		}

		// TODO: Remove if not needed
		//[Test]
		public void Test_RemoveDemand_DemandLeftOver()
		{
			var person = new Person ();

			person.AddDemand (NeedType.Timber, 50);
			person.AddDemand (NeedType.Timber, 25);

			person.RemoveDemand (NeedType.Timber, 50);

			Assert.AreEqual (25, person.GetDemandAmount (NeedType.Timber));
		}
	}
}

