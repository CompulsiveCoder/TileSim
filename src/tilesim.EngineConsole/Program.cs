using System;
using tilesim.Engine;
using tilesim.Engine.Entities;

namespace tilesim.EngineConsole
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var settings = SettingsParser.GetSettings (args);

            settings.OutputType = ConsoleOutputType.Game;
            settings.GameSpeed = 100;

			LaunchGame (settings);
		}


		public static void LaunchGame(EngineSettings settings)
		{
			new GameLauncher ().Launch (settings);
		}
	}
}
