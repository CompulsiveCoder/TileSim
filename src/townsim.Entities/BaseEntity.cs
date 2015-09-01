using System;
using Newtonsoft.Json;

namespace townsim.Entities
{
	public class BaseEntity
	{
		public Guid Id = Guid.NewGuid();

		public BaseEntity ()
		{
			Guid.NewGuid ();
		}

		public string ToJson()
		{
			return JsonConvert.SerializeObject (this);
		}
	}
}

