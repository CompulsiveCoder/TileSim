using System;
using Newtonsoft.Json;
using datamanager.Entities;
using System.Collections.Generic;
using System.Linq;
using tilesim.Engine.Activities;
using tilesim.Engine;
using tilesim.Engine.Needs;

namespace tilesim.Engine.Entities
{
	[Serializable]
	[JsonObject(IsReference = true)]
    public partial class Person : BaseGameEntity, IHasInventory
	{
		public double Age { get; set; }
		public Gender Gender { get; set; }
		public bool IsAlive = true;

        public Inventory Inventory { get; set; }

        public DemandList Demands { get;set; }

		[TwoWay("People")]
		public Building Home{ get; set; }

		[TwoWay("People")]
		public GameTile Tile { get; set; }

		public List<NeedEntry> Needs = new List<NeedEntry>();

        public Dictionary<PersonVital, decimal> Vitals = new Dictionary<PersonVital, decimal> ();

        public Person (EngineSettings settings)
        {
            Demands = new DemandList (this);
            Inventory = new Inventory (this, Demands, settings);

            Vitals.Add (PersonVital.Health, 100);
            Vitals.Add (PersonVital.Thirst, 0);
            Vitals.Add (PersonVital.Hunger, 0);
		}

		public void IncreaseAge(double amount)
		{
			Age += amount;
		}

		public void ValidateProperties()
		{
            throw new NotImplementedException ();
            // TODO: Remove if not needed
			/*if (Age < 0)
				Age = 0;
			if (Thirst < 0)
				Thirst = 0;
			if (Health < 0)
				Health = 0;*/
		}


		public void AddNeed(ActionType actionType, ItemType needType, decimal quantity, decimal priority)
		{
			AddNeed(new NeedEntry (actionType, needType, quantity, priority));
		}

		public void AddNeed(NeedEntry needEntry)
		{
			Needs.Add (needEntry);
		}

		public bool HasNeed(ItemType need)
		{
			return (from n in Needs
			        where n.ItemType == need
			        select n).Count () > 0;
		}

		public bool HasNeed(ActionType actionType, ItemType needType, decimal quantity)
		{
			return (from n in Needs
                where n.ActionType == actionType
                && n.ItemType == needType
				&& n.Quantity == quantity
				select n).Count () > 0;
		}
	}
}

