using System;
using townsim.Entities;
using townsim.Data;

namespace townsim.Engine
{
	public class InstructionEngine
	{
		public InstructionEngine ()
		{
		}

		public void Update(Town town)
		{
			var reader = new EditInstructionReader ();
			var instructions = reader.Read (town.GetType (), town.Id);

			var applier = new InstructionApplier ();

			foreach (var instruction in instructions) {
				applier.Apply (town, instruction);
			}
		}
	}
}

