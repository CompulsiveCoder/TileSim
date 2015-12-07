using System;
using datamanager.Data;

namespace townsim.Data
{
	public class TownKeys
	{
		public string GetTownKey(string townId)
		{
			return DataConfig.Prefix + "-Town-" + townId;
		}
	}
}

