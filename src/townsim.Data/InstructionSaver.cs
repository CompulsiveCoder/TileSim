using System;
using Sider;
using townsim.Entities;
using System.Collections.Generic;

namespace townsim.Data
{
	public class InstructionSaver : BaseDataAdapter
	{
		public InstructionSaver ()
		{
		}

		public void Save(BaseInstruction instruction)
		{
			var client = new RedisClient();
			var key = new InstructionKeys ().GetKey (instruction.Id);

			var json = instruction.ToJson ();
			client.Set(key, json);

			var idManager = new InstructionIdManager ();
			idManager.Add (instruction);

		}
	}
}

