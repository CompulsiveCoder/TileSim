using System;
using System.Web.Script.Serialization;

namespace townsim.Data
{
	public class Parser
	{
		public Parser ()
		{
		}

		public T Parse<T>(string json)
		{
			return new JavaScriptSerializer ().Deserialize<T> (json);
		}
	}
}

