using System;

namespace townsim.Entities
{
	[Serializable]
	public class EngineInfo : BaseEntity
	{
		public EngineSettings Settings;

		public DateTime StartTime { get; set; }

    	public string PlayerId { get; set; }
		
    	public EngineInfo (string engineId, DateTime startTime, EngineSettings settings, string playerId)
		{
			Id = engineId;
			Settings = settings;
			StartTime = startTime;
			PlayerId = playerId;
		}
	}
}

