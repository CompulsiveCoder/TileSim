using System;

namespace tilesim.Engine
{
	public class TilelessException : Exception
	{
		public TilelessException () : base("No tiles were found in the engine.")
		{
		}
	}
}

