using System;
using Sider;

namespace townsim.Data
{
	public class LogWriter
	{
		public LogWriter ()
		{
		}

		public void AppendLine(Guid engineId, string line)
		{
			var client = new RedisClient();
			var key = new LogKeys ().GetLogKey (engineId);
			client.Append (key, line + "\n");
		}

		public string ReadAll(Guid engineId)
		{
			var client = new RedisClient ();
			var key = new LogKeys ().GetLogKey (engineId);
			return client.Get (key);
		}

		public static LogWriter Current = new LogWriter();
	}
}

