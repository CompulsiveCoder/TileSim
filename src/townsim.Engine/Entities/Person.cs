using System;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using datamanager.Entities;
using System.Collections.Generic;
using System.Linq;
using townsim.Engine.Activities;

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

		private ActivityType activityType;
		public ActivityType ActivityType
		{
			get { return activityType; }
			set { activityType = value; }
		}

		[JsonIgnore]
		public BaseActivity Activity { get;set; }

		public Dictionary<string, object> ActivityData = new Dictionary<string, object> ();

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

		public List<SupplyDemand> Demands = new List<SupplyDemand>();

		public Person ()
		{
			Priorities.Add (PriorityTypes.Food, 0);
			Priorities.Add (PriorityTypes.Water, 0);
			Priorities.Add (PriorityTypes.Shelter, 0);

			Supplies.Add (SupplyTypes.Food, 0);
			SuppliesMax.Add (SupplyTypes.Food, 1000);
			Supplies.Add (SupplyTypes.Water, 0);
			SuppliesMax.Add (SupplyTypes.Water, 1000);
			Supplies.Add (SupplyTypes.Wood, 0); // Wood is unrefined timber
			SuppliesMax.Add (SupplyTypes.Wood, 1000);
			Supplies.Add (SupplyTypes.Timber, 0); // Timber is refine wood
			SuppliesMax.Add (SupplyTypes.Timber, 1000);
		}

		public void Start(ActivityType activityType, BaseActivity activity)
		{
			Start (activityType);
			activity.Start ();
		}

		public void Start(ActivityType activity)
		{
			ActivityData.Clear ();
			ActivityType = activity;
			Activity = null;
			ActivityTarget = null;
		}

		public void FinishActivity ()
		{
			ActivityType = ActivityType.Inactive;
			ActivityData.Clear ();
			Activity = null;
			ActivityTarget = null;
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

		public bool Is(ActivityType activity)
		{
			return ActivityType == activity;
		}

		public void AddSupply(SupplyTypes supplyType, decimal amount)
		{
			Supplies [supplyType] = (decimal)Supplies [supplyType] + amount;

			if (GetDemandAmount(supplyType) > 0)
				RemoveDemand (supplyType, amount);
		}

		public void RemoveSupply(SupplyTypes supplyType, decimal amount)
		{
			if (amount > Supplies[supplyType])
				throw new Exception ("There's not enough available. Need " + amount + " but there's only " + Supplies[supplyType] + ".");

			Supplies [supplyType] = Supplies [supplyType] - amount;

			if (Supplies[supplyType] < 0)
				Supplies[supplyType] = 0;
		}

		public bool HasDemand(SupplyTypes supplyType)
		{
			var totalDemand = GetDemandAmount(supplyType);

			//var totalSupply = Supplies [supplyType];

			return totalDemand > 0;
		}

		public void AddDemand(SupplyTypes supply, decimal amount)
		{
			Demands.Add (new SupplyDemand (this, supply, amount));
		}

		public void RemoveDemand(SupplyTypes supply, decimal amountToRemove)
		{
			var totalRemoved = 0.0m;

			while (totalRemoved < amountToRemove) {
				var demandsFound = (from d in Demands
					where d.Supply == supply
					&& d.Amount > 0
					select d).ToArray();
				
				if (demandsFound.Length > 0) {
					var demandFound = demandsFound [0];

					if (demandFound.Amount > amountToRemove) {
						demandFound.Amount -= amountToRemove;

						totalRemoved += amountToRemove;
					}
					else {
						Demands.Remove (demandFound);
						amountToRemove -= demandFound.Amount;

						totalRemoved += demandFound.Amount;
					}
				}
			}
		}

		/*public bool DemandExists(SupplyTypes supply, decimal amount, BaseEntity target)
		{
			return (from demand in Demands
				where demand.Supply == supply
				&& 
		}*/

		public decimal GetDemandAmount(SupplyTypes supplyType)
		{
			return (from demand in Demands
			        where demand.Supply == supplyType
			        select demand.Amount).Sum ();
		}

		public bool Has(SupplyTypes supplyType, decimal amount)
		{
			var value = Supplies [supplyType] >= amount;
			return value;
		}
	}
}

