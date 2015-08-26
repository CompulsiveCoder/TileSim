using System;

namespace townsim.Engine
{
	public class PopulationExpiredException : Exception
	{
		public PopulationExpiredException () : base("There's no-one left. Game over.")
		{
		}
	}
}

