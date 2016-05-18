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

        public Dictionary<PersonVitalType, decimal> Vitals = new Dictionary<PersonVitalType, decimal> ();

        public Person (EngineSettings settings)
        {
            Demands = new DemandList (this);
            Inventory = new Inventory (this, Demands, settings);

            Vitals.Add (PersonVitalType.Energy, 100);
            Vitals.Add (PersonVitalType.Health, 100);
            Vitals.Add (PersonVitalType.Thirst, 0);
            Vitals.Add (PersonVitalType.Hunger, 0);
		}

		public void IncreaseAge(double amount)
		{
			Age += amount;
		}

        public void AddNeed(ActionType actionType, PersonVitalType vitalType, ItemType needType, decimal quantity, decimal priority)
		{
            AddNeed(new NeedEntry (actionType, needType, vitalType, quantity, priority));
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

		public bool HasNeed(ActionType actionType, ItemType needType, PersonVitalType vitalType, decimal quantity)
		{
			return (from n in Needs
                where n.ActionType == actionType
                && (n.ItemType == needType
                    || n.VitalType == vitalType)
				select n).Count () > 0;
		}
	}
}

