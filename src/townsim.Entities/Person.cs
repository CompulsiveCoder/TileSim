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
		public decimal Thirst = 0;
		public decimal Hunger = 0;
		public decimal Health = 100;
		public bool IsAlive = true;
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
		public EmploymentType EmploymentType { get; set; }

		[JsonIgnore]
		public IEmploymentTarget EmploymentTarget { get;set; }

		[JsonIgnore]
		public Town Location { get; set; }

		public Job[] Jobs { get;set; }

		public Person ()
		{
		}

		public void IncreaseAge(double amount)
		{
			Age += amount;
		}

		public void ValidateProperties()
		{
			if (Age < 0)
				Age = 0;
			if (Thirst < 0)
				Thirst = 0;
			if (Health < 0)
				Health = 0;
		}
	}
}

