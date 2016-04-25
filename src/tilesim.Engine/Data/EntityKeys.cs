using System;
using datamanager.Data;

namespace tilesim.Data
{
	public class EntityKeys
	{
		public EntityKeys ()
		{
		}

		public string GetIdsKey(Type entityType)
		{
			return DataConfig.Prefix + "-" + entityType.Name + "-Ids";
		}
	}
}

