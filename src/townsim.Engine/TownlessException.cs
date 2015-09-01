using System;

namespace townsim.Engine
{
	public class TownlessException : Exception
	{
		public TownlessException () : base("No towns were found in the engine.")
		{
		}
	}
}

