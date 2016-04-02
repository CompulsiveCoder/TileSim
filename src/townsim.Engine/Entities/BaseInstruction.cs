using System;

namespace townsim.Engine.Entities
{
	public abstract class BaseInstruction : BaseGameEntity
	{
		public Type TargetType;

		public string TargetId;

		public BaseInstruction ()
		{
		}
	}
}

