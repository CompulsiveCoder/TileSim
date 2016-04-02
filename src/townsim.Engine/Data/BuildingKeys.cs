using System;
using datamanager.Data;

namespace townsim.Data
{
	public class BuildingKeys
	{


		public string GetBuildingsKey(Guid townId)
		{
			return DataConfig.Prefix + "-Town-" + townId.ToString () + "-Buildings";
		}
	}
}

