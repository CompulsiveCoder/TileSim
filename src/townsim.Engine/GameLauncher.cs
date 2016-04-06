using System;
using System.Threading;
using townsim.Engine.Entities;

namespace townsim.Engine
{
	public class GameLauncher
	{
		public GameLauncher ()
		{
		}

		public void Launch(EngineSettings settings)
		{
			Console.WriteLine ("=====================");
			Console.WriteLine ("Launching Game Engine");
			Console.WriteLine ("=====================");



			// TODO: Should this be dependent on "verbose" flag?
			//Console.WriteLine ("Engine ID: " + engineId);

            var context = EngineContext.New (settings);

            context.PopulateFromSettings ();

            context.InitializeCompleteLogic ();

			context.Initialize ();
			//EngineProcess engine = null;
			/*try
			{
				using(var engine = new EngineProcess (engineId))
				{
					engine.Settings.OutputType = ConsoleOutputType.GameSummary;
					engine.Start ();
				}
			}
			catch (GameException ex) {
				Console.WriteLine (ex.Message);
			}*/
			//finally {
			//	if (engine != null)
			//		engine.Dispose ();

		}

		//public EngineContext CreateContext(string engineId)
		//{
			/*var context = new EngineContext (
				CreateEngine()
			);

			return context;*/
		//}
	}
}

