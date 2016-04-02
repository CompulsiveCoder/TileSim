using System;
using NUnit.Framework;
using townsim.Data.Tests;
using townsim.Engine.Entities;

namespace townsim.Engine.Tests.Integration
{
	// TODO: Overhaul and re-enable
	//[TestFixture]
	public class BuildHomeTestFixture : BaseDataTestFixture
	{
		// TODO: Overhaul and re-enable
		//[Test]
		public void Test_BuildHouse_IncludingFellWoodAndMillTimber()
		{

			throw new NotImplementedException ();
			/*var context = EngineContext.New ();
			context.Settings.IsVerbose = true;
			context.Data.IsVerbose = true;

			context.Settings.DefaultTownPopulation = 1;
			context.Settings.GameSpeed = 10;
			context.Settings.ConstructionRate = 10;
			context.Settings.ChoppingRate = 50;
			context.Populate ();

			var person = context.World.People [0];

			context.Start ();

			context.RunCycles (6);

			Assert.IsNotNull (person.Home);
			Assert.AreEqual (100, person.Home.PercentComplete);
			Assert.AreEqual (ActivityType.Inactive, person.Activity);*/
		}
	}
}

