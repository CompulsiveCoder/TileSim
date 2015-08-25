using System;
using townsim.Data;
using townsim.Engine;

namespace townsim.EngineConsole
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var engine = new townsimEngine ();
			engine.Start ();
		}
	}
}
