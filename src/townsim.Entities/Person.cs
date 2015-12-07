using System;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using datamanager.Entities;

namespace townsim.Entities
{
	[Serializable]
	[JsonObject(IsReference = true)]
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

		public bool IsActive
		{
			get { return Activity != ActivityType.Inactive; }
		}

		public ActivityType Activity;

		[JsonIgnore]
		public IActivityTarget ActivityTarget { get;set; }

		[JsonIgnore]
		public Town Location { get; set; }

		public Job[] Jobs { get;set; }

		[TwoWayAttribute("People")]
		public Building Home{ get; set; }

		public bool IsHomeless { get { return Home == null || !Home.IsCompleted; } }

		[TwoWayAttribute("People")]
		public Town Town { get; set; }

		public void Start(ActivityType activity)
		{
			Activity = activity;
		}

		public void Finish ()
		{
			throw new NotImplementedException ();
		}

		public void FocusOn(IActivityTarget target)
		{
			ActivityTarget = target;
		}

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

