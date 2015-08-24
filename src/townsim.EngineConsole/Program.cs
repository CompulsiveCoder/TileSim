using System;
using townsim.Data;

namespace townsim.EngineConsole
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var engine = new Engine ();
			engine.Start ();
		}
	}
}
