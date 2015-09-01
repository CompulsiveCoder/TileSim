using System;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace townsim.Entities
{
	[JsonObject]
	[Serializable]
	public class Person : BaseEntity
	{
		public double Age { get; set; }
		public Gender Gender { get; set; }
		public bool IsAdult
		{
			get { return Age >= 18; }
		}
		public bool IsChild
		{
			get { return !IsAdult; }
		}
		public bool CanWork
		{
			get { return IsAdult; }
		}
		public bool IsEmployed { get; set; }
		public EmploymentType Employment { get; set; }

		public Person ()
		{
		}

		public void IncreaseAge(double amount)
		{
			Age += amount;
		}
	}
}

