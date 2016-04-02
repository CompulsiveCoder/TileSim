using System;
using Sider;

namespace townsim.Data
{
	public class LogWriter
	{
		public RedisClient Client;

		public LogKeys Keys;

		public string EngineId;

		public LogWriter (string engineId)
		{
			Client = new RedisClient ();
			Keys = new LogKeys ("TownSim-" + engineId + "-");
		}

		/*public LogWriter (string prefix)
		{
			Client = new RedisClient ();
			Keys = new LogKeys (prefix);
		}*/

		public void WriteLine(string line)
		{
			var key = Keys.GetLogKey (EngineId);
			Client.Append (key, line + "\n");
			Console.WriteLine (line);
		}

		public string ReadAll(string engineId)
		{
			var key = Keys.GetLogKey (engineId);
			return Client.Get (key);
		}
	}
}

