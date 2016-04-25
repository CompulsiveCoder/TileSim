using System;

namespace tilesim.Engine
{
	public class PopulationExpiredException : Exception
	{
		public PopulationExpiredException () : base("There's no-one left. Game over.")
		{
		}
	}
}

