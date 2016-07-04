using System;
using tilesim.Engine;
using datamanager.Data;
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
			Settings = settings;
			StartTime = startTime;
		}
	}
}

