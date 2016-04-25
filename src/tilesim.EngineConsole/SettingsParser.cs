using System;
using tilesim.Engine.Entities;

namespace tilesim.EngineConsole
{
	public class SettingsParser
	{
		static public EngineSettings GetSettings(string[] args)
		{
			var arguments = new Arguments (args);

			var settings = new EngineSettings ();

			settings.EngineId = GetEngineId (arguments);

            if (arguments.ContainsAny ("v", "verbose"))
                settings.OutputType = tilesim.Engine.ConsoleOutputType.Debug;

			if (arguments.ContainsAny ("speed"))
				settings.GameSpeed = arguments.GetInt("speed");

			if (settings.IsVerbose) {
				// TODO: Output settings summary
			}

			return settings;
		}

		static public string GetEngineId(Arguments arguments)
		{
			var engineId = String.Empty;

			if (arguments.KeylessArguments.Length == 1)
				engineId = arguments.KeylessArguments [0];
			else
				engineId = Guid.NewGuid ().ToString();

			return engineId;
		}
	}
}

