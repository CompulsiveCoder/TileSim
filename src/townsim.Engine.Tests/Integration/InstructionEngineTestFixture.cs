using System;
using NUnit.Framework;
using townsim.Entities;
using townsim.Data;
using datamanager.Data;

namespace townsim.Engine.Tests.Integration
{
	[TestFixture]
	public class InstructionEngineTestFixture
	{
		[Test]
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

