using System;
using Sider;

namespace townsim.Data
{
	public class LogWriter
	{
		public RedisClient Client;

		public LogWriter ()
		{
			Client = new RedisClient ();
		}

		public void AppendLine(string engineId, string line)
		{
			var key = new LogKeys ().GetLogKey (engineId);
			Client.Append (key, line + "\n");
			Console.WriteLine (line);
		}

		public string ReadAll(string engineId)
		{
			var key = new LogKeys ().GetLogKey (engineId);
			return Client.Get (key);
		}

		public static LogWriter Current = new LogWriter();
	}
}

