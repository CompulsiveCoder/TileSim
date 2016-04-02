using System;
using System.Threading;
using townsim.Entities;

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

			var context = new GameCreator(settings).Create();

			context.Populate ();

			context.Start ();
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

