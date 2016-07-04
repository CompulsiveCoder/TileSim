using System;
using NUnit.Framework;
using tilesim.Engine.Entities;
using tilesim.Engine.Needs;

namespace tilesim.Engine.Tests.Unit.Entities
{
	// TODO: Remove if not needed
	//[TestFixture]
	public class PersonTestFixture
	{
		// TODO: Remove if not needed
		//[Test]
		public void Test_RemoveDemand_SameAmount_SingleDemand()
		{
            throw new NotImplementedException ();
			/*var person = new Person ();

			person.AddDemand (ItemType.Timber, 100);

			person.RemoveDemand (ItemType.Timber, 100);

			var remainingAmount = person.GetDemandAmount (ItemType.Timber);

			Assert.AreEqual (0, remainingAmount);*/
		}

		// TODO: Remove if not needed
		//[Test]
		public void Test_RemoveDemand_SameAmount_MultipleDemands()
		{
            throw new NotImplementedException ();
			/*var person = new Person ();

			person.AddDemand (ItemType.Timber, 50);
			person.AddDemand (ItemType.Timber, 25);
			person.AddDemand (ItemType.Timber, 25);

			person.RemoveDemand (ItemType.Timber, 100);

			var remainingAmount = person.GetDemandAmount (ItemType.Timber);

			Assert.AreEqual (0, remainingAmount);*/
		}

		// TODO: Remove if not needed
		//[Test]
		public void Test_RemoveDemand_DemandLeftOver()
		{
            throw new NotImplementedException ();
			/*var person = new Person ();

			person.AddDemand (ItemType.Timber, 50);
			person.AddDemand (ItemType.Timber, 25);

			person.RemoveDemand (ItemType.Timber, 50);

			Assert.AreEqual (25, person.GetDemandAmount (ItemType.Timber));*/
		}
	}
}

