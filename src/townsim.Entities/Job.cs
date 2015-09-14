using System;

namespace townsim.Entities
{
	public class Job
	{
		public double PercentComplete = 0;

		public EmploymentType Type { get; set; }

		public Job (EmploymentType employmentType)
		{
			Type = employmentType;
		}
	}
}

