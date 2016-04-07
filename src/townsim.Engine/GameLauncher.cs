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
			//Console.WriteDebugLine ("Engine ID: " + engineId);

            var context = EngineContext.New (settings);

            context.Settings.OutputType = ConsoleOutputType.Game;

            context.PopulateFromSettings ();

            context.AddCompleteLogic ();

			context.Initialize ();
			//EngineProcess engine = null; // TODO: Remove if not needed
			try
			{
				using(var engine = new EngineProcess (context))
				{
                    engine.Run();
				}
			}
			catch (GameException ex) {
                context.Console.WriteDebugLine (ex.Message);
			}
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

