using System;

namespace townsim.Entities
{
	public class EngineSettings
	{
		public int GameSpeed = 60 * 60;//*24;
		public int Interval = 1000; // milliseconds

		public EngineSettings()
		{
		}

		public EngineSettings(int gameSpeed)
		{
			GameSpeed = gameSpeed;
		}
	}
}

