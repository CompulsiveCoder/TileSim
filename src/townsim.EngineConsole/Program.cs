using System;
using townsim.Data;
using townsim.Engine;

namespace townsim.EngineConsole
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var engineId = String.Empty;

			if (args.Length == 1)
				engineId = args [0];
			else
				engineId = Guid.NewGuid ().ToString();

			Console.WriteLine ("Engine ID: " + engineId);

			townsimEngine engine = null;
			try
			{
				using(engine = new townsimEngine (engineId))
				{
					engine.Settings.OutputType = ConsoleOutputType.GameSummary;
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
