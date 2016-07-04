using System;
using Newtonsoft.Json;

namespace tilesim.Engine.Entities
{
	[Serializable]
	[JsonObject(IsReference = true)]
	public class BaseGameEntity : datamanager.Entities.BaseEntity
	{
		public BaseGameEntity ()
		{
			
		}
	}
}

