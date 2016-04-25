using System;

namespace tilesim.Engine.Entities
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

