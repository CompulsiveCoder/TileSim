using System;
using datamanager.Data;

namespace tilesim.Data
{
	public class LogKeys
	{
		public string Prefix;

		public LogKeys (string prefix)
		{
			Prefix = prefix;
		}

		public string GetLogKey(string engineId)
		{
			return Prefix + "-Log-" + engineId;
		}
	}
}

