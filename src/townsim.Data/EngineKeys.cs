using System;

namespace townsim.Data
{
	public class EngineKeys
	{
		public EngineKeys ()
		{
		}

		public string GetEngineIdsKey()
		{
			return "TownSim-Engines-Ids";
		}

		public string GetInfoKey(Guid engineId)
		{
			return "TownSim-Engine-" + engineId.ToString();
		}
	}
}

