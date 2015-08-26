using System;
using townsim.Data;
using townsim.Engine;

namespace townsim.EngineConsole
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			try
			{
			var engine = new townsimEngine ();
			engine.Start ();
			}
			catch (GameException ex) {
				Console.WriteLine (ex.Message);
			}
		}
	}
}
