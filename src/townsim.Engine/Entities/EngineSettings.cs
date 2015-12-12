using System;
using townsim.Engine;

namespace townsim.Entities
{
	[Serializable]
	public class EngineSettings
	{
		public int GameSpeed = 10;//60 * 60 * 24;
		public int Interval = 1000; // milliseconds

		public decimal TimberWasteRate = 3;

		public ConsoleOutputType OutputType = ConsoleOutputType.General;

		public EngineSettings()
		{
		}

		public EngineSettings(int gameSpeed)
		{
			GameSpeed = gameSpeed;
		}
	}
}

