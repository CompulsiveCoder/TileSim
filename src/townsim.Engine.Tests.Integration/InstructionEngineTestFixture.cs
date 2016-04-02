using System;
using NUnit.Framework;
using townsim.Engine.Entities;
using townsim.Data;
using datamanager.Data;

namespace townsim.Engine.Tests.Integration
{
	// TODO: Overhaul and re-enable
	//[TestFixture]
	public class InstructionEngineTestFixture
	{
		// TODO: Overhaul and re-enable
		//[Test]
		public void Test_Update()
		{
			var town = new Town ();

			var instruction = new EditInstruction (town.GetType (), town.Id, "TreesToPlantPerDay", 5);

			new DataManager ().Save (instruction);

			var instructionEngine = new InstructionEngine ();

			instructionEngine.Update (town);

			Assert.AreEqual (5, town.TreesToPlantPerDay);
		}
	}
}

