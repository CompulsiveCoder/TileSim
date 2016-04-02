using System;
using townsim.Data;
using townsim.Engine;
using townsim.Engine.Entities;

namespace townsim.EngineConsole
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var settings = SettingsParser.GetSettings (args);

			LaunchGame (settings);
		}


		public static void LaunchGame(EngineSettings settings)
		{
			new GameLauncher ().Launch (settings);
		}
	}
}
