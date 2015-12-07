using System;
using townsim.Entities;
using Sider;
using System.Collections.Generic;
using System.Linq;

namespace townsim.Data
{
	public class EditInstructionReader
	{
		public EditInstructionReader ()
		{
		}

		public EditInstruction[] Read(Type targetType, string targetId)
		{
			var data = new datamanager.Data.DataManager ();

			var instructions = (from instruction in data.Get<EditInstruction> ()
					where instruction.TargetType == targetType
				&& instruction.TargetId == targetId
				select instruction).ToArray();

			return instructions;
			/*var client = new RedisClient ();
			var key = new InstructionKeys ().GetIdsKey (targetType, targetId);

			var instructions = new List<EditInstruction> ();

			if (!client.Exists (key))
				return new EditInstruction[]{ };
			else {
				var idsString = client.Get (key);

				var idsParts = idsString.Split ('.');

				foreach (var idString in idsParts) {
					if (!String.IsNullOrEmpty (idString.Trim ())) {
						var id = Guid.Parse (idString);

						var instruction = Read (id);

						instructions.Add (instruction);
					}
				}

				return instructions.ToArray ();
			}*/
		}

		public EditInstruction Read(string instructionId)
		{
			return new datamanager.Data.DataManager ().Get<EditInstruction> (instructionId);
			/*var client = new RedisClient ();

			var key = new InstructionKeys ().GetKey (instructionId);

			var json = client.Get (key);

			return JsonToEntity<EditInstruction> (json);*/
		}
	}
}

