using System;
using tilesim.Entities;
using Sider;

namespace tilesim.Data
{
	public class InstructionIdManager : BaseDataAdapter
	{
		public InstructionIdManager ()
		{
		}

		public void Add(BaseInstruction instruction)
		{
			var key = new InstructionKeys ().GetIdsKey (instruction.TargetType, instruction.TargetId);

			var client = new RedisClient ();

			var stringToAppend = instruction.Id.ToString();

			if (client.Exists (key))
				stringToAppend = "." + stringToAppend;
			
			client.Append (key, stringToAppend);
		}
	}
}

