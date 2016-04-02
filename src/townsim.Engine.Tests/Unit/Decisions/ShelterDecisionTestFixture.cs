using System;
using NUnit.Framework;
using townsim.Engine.Entities;
using townsim.Engine.Needs;
using townsim.Engine.Activities;
using townsim.Data.Tests;

namespace townsim.Engine.Tests.Unit.Decisions
{
	//[TestFixture]
	public class ShelterDecisionTestFixture : BaseDataTestFixture
	{
		// TODO: Remove if not needed
		//[Test]
		public void Test_Decide_ShelterNeeded()
		{
			var person = new Person ();

			person.AddNeed(ItemType.Shelter, 1, 100);

			var decision = new ShelterDecision (EngineSettings.DefaultVerbose);

			decision.Decide (person);

			var expectedName = typeof(BuildShelterActivity).Name;

			var foundName = person.Activity.GetType ().Name;

			Assert.AreEqual(expectedName, foundName);
		}
	}
}

