using System;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using datamanager.Entities;
using System.Collections.Generic;

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
			get { return ActivityType != ActivityType.Inactive; }
		}

		public ActivityType ActivityType;

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

		public Dictionary<PriorityTypes, decimal> Priorities = new Dictionary<PriorityTypes, decimal> ();

		public Dictionary<SupplyTypes, decimal> Supplies = new Dictionary<SupplyTypes, decimal> ();

		public Dictionary<SupplyTypes, decimal> SuppliesMax = new Dictionary<SupplyTypes, decimal> ();

		public Person ()
		{
			Priorities.Add (PriorityTypes.Food, 0);
			Priorities.Add (PriorityTypes.Water, 0);
			Priorities.Add (PriorityTypes.Shelter, 0);

			Supplies.Add (SupplyTypes.Food, 10);
			SuppliesMax.Add (SupplyTypes.Food, 1000);
			Supplies.Add (SupplyTypes.Water, 0);
			SuppliesMax.Add (SupplyTypes.Water, 1000);
			Supplies.Add (SupplyTypes.Timber, 50);
		}

		public void Start(ActivityType activity)
		{
			ActivityType = activity;
		}

		public void Finish ()
		{
			ActivityType = ActivityType.Inactive;
		}

		public void FocusOn(IActivityTarget target)
		{
			ActivityTarget = target;
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

