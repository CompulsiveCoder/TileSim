using System;

namespace townsim.Data
{
	public class TownKeys
	{
		public string GetTownKey(Guid townId)
		{
			return DataConfig.Prefix + "-Town-" + townId.ToString ();
		}
	}
}

