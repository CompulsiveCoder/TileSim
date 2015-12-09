using System;

namespace townsim.Entities
{
	[Serializable]
	public class Job
	{
		public double PercentComplete = 0;

		public ActivityType Type { get; set; }

		public Job (ActivityType employmentType)
		{
			Type = employmentType;
		}
	}
}

