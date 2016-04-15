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

            settings.OutputType = ConsoleOutputType.Game;
            settings.GameSpeed = 10;

			LaunchGame (settings);
		}


		public static void LaunchGame(EngineSettings settings)
		{
			new GameLauncher ().Launch (settings);
		}
	}
}
