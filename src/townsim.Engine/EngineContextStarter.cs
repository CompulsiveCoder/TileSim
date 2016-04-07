using System;

namespace townsim.Engine
{
	public class EngineContextStarter
	{
		public EngineContext Context { get;set; }

		public EngineContextStarter (EngineContext context)
		{
			Context = context;
		}

		public void Start()
		{
			throw new NotImplementedException ();

			/*try
			{
				using(var engine = new EngineProcess ())
				{
					// TODO: Reimplement
					//engine.Settings.OutputType = ConsoleOutputType.GameSummary;
					engine.Start ();
				}
			}
			catch (GameException ex) {
				Console.WriteDebugLine (ex.Message);
			}*/
		}
	}
}

