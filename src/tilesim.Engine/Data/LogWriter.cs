using System;
using Sider;
using datamanager.Data;

namespace tilesim.Data
{
	public class LogWriter
	{
        public BaseRedisClientWrapper Client;

		public LogKeys Keys;

		public string EngineId;

        public LogWriter (string engineId, BaseRedisClientWrapper client)
		{
            Client = client;
            
			Keys = new LogKeys ("TileSim-" + engineId + "-");
		}

		/*public LogWriter (string prefix)
		{
			Client = new RedisClient ();
			Keys = new LogKeys (prefix);
		}*/

		public void WriteLine(string line)
		{
            throw new NotImplementedException ();
			/*var key = Keys.GetLogKey (EngineId);
			Client.Append (key, line + "\n");
            Context.Console.WriteDebugLine (line);*/
		}

		public string ReadAll(string engineId)
		{
			var key = Keys.GetLogKey (engineId);
			return Client.Get (key);
		}
	}
}

