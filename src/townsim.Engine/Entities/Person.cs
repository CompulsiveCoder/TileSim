using System;
using Newtonsoft.Json;
using datamanager.Entities;
using System.Collections.Generic;
using System.Linq;
using townsim.Engine.Activities;
using townsim.Engine;
using townsim.Engine.Needs;

namespace townsim.Engine.Entities
{
	[Serializable]
	[JsonObject(IsReference = true)]
	public partial class Person : BaseGameEntity
	{
		public double Age { get; set; }
		public Gender Gender { get; set; }
		public decimal Thirst = 0;
		public decimal Hunger = 0;
		public decimal Health = 100;
		public bool IsAlive = true;

		// TODO: Remove. Use the activity object itself to store data. This is obsolete
		public Dictionary<string, object> ActivityData = new Dictionary<string, object> ();

		[JsonIgnore]
		public Town Location { get; set; }

        // TODO
		//public Job[] Jobs { get;set; }

		[TwoWayAttribute("People")]
		public Building Home{ get; set; }

		[TwoWayAttribute("People")]
		public Town Town {
			get;
			set;
		}

		[TwoWay("People")]
		public GameTile Tile { get; set; }

		// TODO: Remove
		//public Dictionary<PriorityTypes, decimal> Priorities = new Dictionary<PriorityTypes, decimal> ();

		public Dictionary<ItemType, decimal> Supplies = new Dictionary<ItemType, decimal> ();

		public Dictionary<ItemType, decimal> SuppliesMax = new Dictionary<ItemType, decimal> ();

		public List<ItemDemand> Demands = new List<ItemDemand>();

		//public Dictionary<NeedType, decimal> Needs = new Dictionary<NeedType, decimal>();
		public List<NeedEntry> Needs = new List<NeedEntry>();

		public List<BaseActivity> ActivityQueue = new List<BaseActivity>();

		public string ActivityName {
			get {
				if (Activity == null)
					return String.Empty;
				else
					return Activity.GetType ().Name;
			}
		}

		public BaseActivity Activity {
			get {
				if (ActivityQueue.Count == 0)
					return null;
				else
					return ActivityQueue [0];	
			}
		}

		public void RushActivity(BaseActivity activity)
		{
			ActivityQueue.Insert(0, activity);
		}

		public void AddActivity(BaseActivity activity)
		{
			ActivityQueue.Add (activity);
		}

		public void FinishedActivity(BaseActivity activity)
		{
			ActivityQueue.Remove (activity);
		}

        // TODO: Remove if not needed
		//public List<BaseDecision> Decisions = new List<BaseDecision> ();

		public Person ()
        {
            Supplies.Add (ItemType.Shelter, 0);
            SuppliesMax.Add (ItemType.Shelter, 1);
			Supplies.Add (ItemType.Food, 0);
			SuppliesMax.Add (ItemType.Food, 1000);
			Supplies.Add (ItemType.Water, 0);
			SuppliesMax.Add (ItemType.Water, 1000);
			Supplies.Add (ItemType.Wood, 0); // Wood is unrefined timber
			SuppliesMax.Add (ItemType.Wood, 1000);
			Supplies.Add (ItemType.Timber, 0); // Timber is refine wood
			SuppliesMax.Add (ItemType.Timber, 1000);
		}

		public void Assign(ActivityType activityType)
		{
			throw new NotImplementedException ();
			//this.activityType = activityType;
		}

		public void Assign(BaseActivity activity)
		{
			throw new NotImplementedException ();
			/*
			this.activity = activity;
			
			this.activityType = activity.Type;

			if (activity.Actor == null)
				activity.Actor = this;*/
		}

		public void ClearActivity()
		{

			throw new NotImplementedException ();
			/*activity = null;
			activityType = ActivityType.Inactive;
			ActivityData.Clear ();*/
		}

		/*public void Start(ActivityType activityType, BaseActivity activity)
		{
			Start (activityType);
			activity.Init (activityType, this);
			activity.Start ();
		}

		public void Start(ActivityType activity)
		{
			ActivityData.Clear ();
			ActivityType = activity;
		}*/

		/*public void FinishActivity ()
		{
			ActivityType = ActivityType.Inactive;
			ActivityData.Clear ();
			Activity = null;
		}*/

		/*public void FocusOn(IActivityTarget target)
		{
			throw new NotImplementedException ();
			//ActivityTarget = target;
		}*/


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

			throw new NotImplementedException ();
			//return ActivityType == activity;
		}

		#region Supplies
		public void AddSupply(ItemType needType, decimal amount)
		{
			Supplies [needType] = (decimal)Supplies [needType] + amount;

			if (GetDemandAmount(needType) > 0)
				RemoveDemand (needType, amount);
		}

		public void RemoveSupply(ItemType needType, decimal amount)
		{
			if (amount > Supplies[needType])
				throw new Exception ("There's not enough available. Need " + amount + " but there's only " + Supplies[needType] + ".");

			Supplies [needType] = Supplies [needType] - amount;

			if (Supplies[needType] < 0)
				Supplies[needType] = 0;
		}

		public bool Has(ItemType needType, decimal amount)
		{
			var value = Supplies [needType] >= amount;
			return value;
		}
		#endregion

		#region Demands
		public bool HasDemand(ItemType needType)
		{
			var totalDemand = GetDemandAmount(needType);

			return totalDemand > 0;
		}

		public void AddDemand(ItemType supply, decimal amount)
		{
			Demands.Add (new ItemDemand (this, supply, amount));
		}

		public void RemoveDemand(ItemType supply, decimal amountToRemove)
		{
			var totalRemoved = 0.0m;

			var amountRemainingToRemove = amountToRemove;

			while (totalRemoved < amountToRemove
				&& HasDemand(supply)) {
				var demandsFound = (from d in Demands
					where d.Supply == supply
					&& d.Amount > 0
					select d).ToArray();
				
				if (demandsFound.Length > 0) {
					var demandFound = demandsFound [0];

					if (demandFound.Amount > amountToRemove) {
						demandFound.Amount -= amountRemainingToRemove;

						totalRemoved += amountToRemove;
					}
					else {
						Demands.Remove (demandFound);
						amountRemainingToRemove -= demandFound.Amount;

						totalRemoved += demandFound.Amount;
					}
				}
			}
		}

		public decimal GetDemandAmount(ItemType needType)
		{
			return (from demand in Demands
			        where demand.Supply == needType
			        select demand.Amount).Sum ();
		}
		#endregion


		public void AddNeed(ItemType needType, decimal quantity, decimal priority)
		{
			AddNeed(new NeedEntry (needType, quantity, priority));
		}

		public void AddNeed(NeedEntry needEntry)
		{
			Needs.Add (needEntry);
		}

		public bool HasNeed(ItemType need)
		{
			return (from n in Needs
			        where n.Type == need
			        select n).Count () > 0;
		}

		public bool HasNeed(ItemType needType, decimal quantity)
		{
			return (from n in Needs
				where n.Type == needType
				&& n.Quantity == quantity
				select n).Count () > 0;
		}
	}
}

