using System;
using townsim.Entities;
using datamanager.Data;

namespace townsim.Data
{
	public class InstructionKeys
	{
		public string GetKey(Guid instructionId)
		{
			return DataConfig.Prefix + "-Instruction-" + instructionId;
		}

		public string GetIdsKey(Type targetType, Guid targetId)
		{
			return DataConfig.Prefix + "-Instructions-" + targetType.Name + "-" + targetId;
		}
	}
}

