using System;
using Newtonsoft.Json;

namespace townsim.Entities
{
	[Serializable]
	[JsonObject(IsReference = true)]
	public class BaseEntity : datamanager.Entities.BaseEntity
	{
		public BaseEntity ()
		{
			
		}
	}
}

