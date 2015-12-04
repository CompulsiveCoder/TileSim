using System;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace townsim.Data
{
	public class Parser
	{
		public Parser ()
		{
		}

		public T Parse<T>(string json)
		{
			if (String.IsNullOrEmpty (json))
				return default(T);
			else
				return JsonConvert.DeserializeObject<T>(json);
		}
	}
}

