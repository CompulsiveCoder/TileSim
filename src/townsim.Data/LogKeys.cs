using System;

namespace townsim.Data
{
	public class LogKeys
	{
		public LogKeys ()
		{
		}

		public string GetLogKey(Guid engineId)
		{
			return DataConfig.Prefix + "-Log-" + engineId.ToString();
		}
	}
}

