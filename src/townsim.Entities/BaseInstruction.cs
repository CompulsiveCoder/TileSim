using System;

namespace townsim.Entities
{
	public abstract class BaseInstruction : BaseEntity
	{
		public Type TargetType;

		public Guid TargetId;

		public BaseInstruction ()
		{
		}
	}
}

