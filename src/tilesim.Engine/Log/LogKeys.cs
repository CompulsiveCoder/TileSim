using System;
using datamanager.Data;

namespace tilesim.Log
{
    // TODO: Should this be moved to Data project or should namespace be changed to .Engine?
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

