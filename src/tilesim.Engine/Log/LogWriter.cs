using System;
using Sider;
using datamanager.Data;
using datamanager.Data.Providers;

namespace tilesim.Log
{
    // TODO Overhaul
	public class LogWriter
	{
        public BaseDataProvider Provider;

		public LogKeys Keys;

		public string EngineId;

        public LogWriter (string engineId, BaseDataProvider provider)
		{
            Provider = provider;
            
			Keys = new LogKeys ("TileSim-" + engineId + "-");
		}

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
			return Provider.Get (key);
		}
	}
}

