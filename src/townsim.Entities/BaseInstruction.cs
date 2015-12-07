using System;

namespace townsim.Entities
{
	public abstract class BaseInstruction : BaseEntity
	{
		public Type TargetType;

		public string TargetId;

		public BaseInstruction ()
		{
		}
	}
}

