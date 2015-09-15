using System;

namespace townsim.Engine
{
	public class EngineSettings
	{
		public double GameSpeed = 60 * 60;

		public EngineSettings()
		{
		}

		public EngineSettings(double gameSpeed)
		{
			GameSpeed = gameSpeed;
		}
	}
}

