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
			else {
				var id = Guid.NewGuid ().ToString ();
				engineId = id.Substring (0, id.IndexOf ("-"));
			}

			Console.WriteLine ("Engine ID: " + engineId);

			townsimEngine engine = null;
			try
			{
				using(engine = new townsimEngine (engineId))
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
