using System;
using NUnit.Framework;
using tilesim.Engine.Entities;
using tilesim.Data;
using datamanager.Data;

namespace tilesim.Engine.Tests.Integration
{
	// TODO: Overhaul and re-enable
	//[TestFixture]
	public class InstructionEngineTestFixture
	{
		// TODO: Overhaul and re-enable
		//[Test]
		public void Test_Update()
		{
			var tile = new Tile ();

			var instruction = new EditInstruction (tile.GetType (), tile.Id, "TreesToPlantPerDay", 5);

			new DataManager ().Save (instruction);

			var instructionEngine = new InstructionEngine ();

			instructionEngine.Update (tile);

			Assert.AreEqual (5, tile.TreesToPlantPerDay);
		}
	}
}

