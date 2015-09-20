using System;

namespace townsim.Entities
{
	public class EngineInfo : BaseEntity
	{
		public EngineSettings Settings;

		public DateTime StartTime { get; set; }
		
		public EngineInfo (Guid engineId, DateTime startTime, EngineSettings settings)
		{
			Id = engineId;
			Settings = settings;
			StartTime = startTime;
		}
	}
}

