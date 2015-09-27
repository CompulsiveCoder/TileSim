using System;

namespace townsim.Entities
{
	public class EngineInfo : BaseEntity
	{
		public EngineSettings Settings;

		public DateTime StartTime { get; set; }

    public Guid PlayerId { get; set; }
		
    public EngineInfo (Guid engineId, DateTime startTime, EngineSettings settings, Guid playerId)
		{
			Id = engineId;
			Settings = settings;
			StartTime = startTime;
      PlayerId = playerId;
		}
	}
}

