using System;
using NUnit.Framework;
using tilesim.Engine.Entities;

namespace tilesim.Data.Tests.Unit
{
	[TestFixture]
	public class InstructionApplierTestFixture
	{
		[Test]
		public void Test_Apply_EditInstruction()
		{
			// TODO: Use a test entity rather than the tile entity to reduce processing cost

            throw new NotImplementedException ();
			/*var tile = new GameTile ();
			tile.TreesToPlantPerDay = 1;

			var instruction = new EditInstruction (tile.GetType(), tile.Id, "TreesToPlantPerDay", 5);

			var applier = new InstructionApplier ();

			applier.Apply (tile, instruction);

			Assert.AreEqual (5, tile.TreesToPlantPerDay);*/
		}
	}
}

