using System;
using Newtonsoft.Json;

namespace townsim.Entities
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

