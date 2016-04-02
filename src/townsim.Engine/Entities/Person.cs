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
    public partial class Person : BaseGameEntity, IHasInventory
	{
		public double Age { get; set; }
		public Gender Gender { get; set; }
		public decimal Thirst = 0;
		public decimal Hunger = 0;
		public decimal Health = 100;
		public bool IsAlive = true;

        public Inventory Inventory { get; set; }

        public DemandList Demands { get;set; }

		[JsonIgnore]
		public Town Location { get; set; }

		[TwoWayAttribute("People")]
		public Building Home{ get; set; }

		[TwoWayAttribute("People")]
		public Town Town {
			get;
			set;
		}

		[TwoWay("People")]
		public GameTile Tile { get; set; }

		public List<NeedEntry> Needs = new List<NeedEntry>();

        public Person (EngineSettings settings)
        {
            Demands = new DemandList (this);
            Inventory = new Inventory (this, Demands, settings);
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

