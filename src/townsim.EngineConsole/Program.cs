using System;
using townsim.Data;
using townsim.Engine;

namespace townsim.EngineConsole
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			townsimEngine engine = null;
			try
			{
				using(engine = new townsimEngine ())
				{
				engine.Start ();
				}
			}
			catch (GameException ex) {
				Console.WriteLine (ex.Message);
			}
			finally {
				if (engine != null)
					engine.Dispose ();
			}
		}
	}
}
