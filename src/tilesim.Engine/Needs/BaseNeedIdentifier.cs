using System;
using tilesim.Engine.Entities;
using tilesim.Engine.Needs;
using System.Collections.Generic;
using tilesim.Engine.Activities;

namespace tilesim.Engine
{
	public abstract class BaseNeedIdentifier
	{
        public ActivityVerb ActionType { get;set; }

		public ItemType ItemType { get; set; }

        public PersonVitalType VitalType { get; set; }

		public decimal DefaultPriority { get;set; }

		public EngineSettings Settings { get; set; }

        public List<NeedEntry> Needs = new List<NeedEntry>();

        public ConsoleHelper Console { get; set; }

        public BaseNeedIdentifier(ActivityVerb actionType, ItemType itemType, PersonVitalType vitalType, EngineSettings settings, ConsoleHelper console)
		{
            ActionType = actionType;
			ItemType = itemType;
            VitalType = vitalType;
			Settings = settings;
            Console = console;

            if (vitalType != PersonVitalType.NotSet)
                DefaultPriority = settings.DefaultVitalPriorities [vitalType];
            else if (itemType != ItemType.NotSet)
                DefaultPriority = settings.DefaultItemPriorities [itemType];
		}

		public abstract bool IsNeeded (Person person);

        public abstract void RegisterNeed(Person person, ActivityVerb actionType, ItemType needType, PersonVitalType vitalType, decimal priority);

		public virtual void RegisterIfNeeded(Person person)
		{            
            var priority = DefaultPriority;

            var needIsNotAlreadyRegistered = !NeedIsRegistered (person, ActionType, ItemType, VitalType, priority);

            var requiresRegistration = (IsNeeded(person) && needIsNotAlreadyRegistered);

			if (requiresRegistration)
                RegisterNeed(person, ActionType, ItemType, VitalType, priority);

            CommitNeeds (person);
		}

        public virtual bool NeedIsRegistered (Person person, ActivityVerb actionType, ItemType needType, PersonVitalType vitalType, decimal quantity)
        {
            return person.HasNeed (actionType, needType, vitalType, quantity);
        }

        public void AddNeed(ActivityVerb actionType, ItemType itemType, PersonVitalType vitalType, decimal quantity, decimal priority)
        {
            if (Settings.IsVerbose)
                Console.WriteDebugLine ("    Registering the need to " + actionType + " " + quantity + " " + itemType + ".");

            Needs.Add (new NeedEntry (actionType, itemType, vitalType, quantity, priority));
        }

        public void CommitNeeds(Person person)
        {
            if (Settings.IsVerbose) {
                Console.WriteDebugLine ("    Committing needs");
            }

            while (Needs.Count > 0)
            {
                var need = Needs [0];

                person.Needs.Add (need);

                Needs.RemoveAt (0);               
            }
        }

	}
}

