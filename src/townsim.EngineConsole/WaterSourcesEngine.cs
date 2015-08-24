using System;
using townsim.Data;

namespace townsim.EngineConsole
{
	public class WaterSourcesEngine
	{
		public WaterSourcesEngine ()
		{
		}

		public void Update(Town town)
		{
			var rain = 100;
			town.WaterSources = town.WaterSources + rain;
		}
	}
}

