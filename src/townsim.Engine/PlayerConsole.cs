using System;
using townsim.Engine.Entities;

namespace townsim.Engine
{
	public class PlayerConsole
	{
		public EngineContext Context { get;set; }

		public PlayerConsole(EngineContext context)
		{
			Context = context;
		}

		public void Write(Person player, string text)
		{
			throw new NotImplementedException ();
			/*if (player.Id == Settings.PlayerId) {
				Console.Write (text);
			}*/
		}

		public void WriteLine(Person player, string text)
		{
			throw new NotImplementedException ();
			/*if (player.Id == Settings.PlayerId) {
				Console.WriteText (text);
			}*/
		}
	}
}