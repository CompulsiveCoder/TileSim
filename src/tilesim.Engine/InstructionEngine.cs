using System;
using tilesim.Engine.Entities;
using tilesim.Data;

namespace tilesim.Engine
{
	public class InstructionEngine
	{
		public InstructionEngine ()
		{
		}

		public void Update(Tile tile)
		{
			var reader = new EditInstructionReader ();
			var instructions = reader.Read (tile.GetType (), tile.Id);

			var applier = new InstructionApplier ();

			foreach (var instruction in instructions) {
				applier.Apply (tile, instruction);
			}
		}
	}
}

