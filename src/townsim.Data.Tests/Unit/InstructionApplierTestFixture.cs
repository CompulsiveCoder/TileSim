using System;
using NUnit.Framework;
using townsim.Entities;

namespace townsim.Data.Tests.Unit
{
	[TestFixture]
	public class InstructionApplierTestFixture
	{
		[Test]
		public void Test_Apply_EditInstruction()
		{
			// TODO: Use a test entity rather than the town entity to reduce processing cost

			var town = new Town ();
			town.TreesToPlantPerDay = 1;

			var instruction = new EditInstruction (town.GetType(), town.Id, "TreesToPlantPerDay", 5);

			var applier = new InstructionApplier ();

			applier.Apply (town, instruction);

			Assert.AreEqual (5, town.TreesToPlantPerDay);
		}
	}
}

