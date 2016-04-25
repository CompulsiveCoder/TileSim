using System;
using tilesim.Engine;
using datamanager.Data;
using tilesim.Data;
using Newtonsoft.Json;

namespace tilesim.Engine.Entities
{
	[Serializable]
	public class EngineInfo : BaseGameEntity
	{
		public DateTime StartTime { get; set; }

		public EngineSettings Settings;

		public static EngineInfo Default
		{
			get
			{	
				return new EngineInfo (DateTime.Now, EngineSettings.Default);
			}
		}
		
    	public EngineInfo (DateTime startTime, EngineSettings settings)
		{
		//	throw new NotImplementedException ();
			Settings = settings;
			StartTime = startTime;
		}

		/*public EngineInfo(EngineSettings settings, EngineClock clock, DataManager data, LogWriter log)
		{
			Settings = settings;
			Clock = clock;
			Data = data;
			Log = log;
		}*/
	}
}

