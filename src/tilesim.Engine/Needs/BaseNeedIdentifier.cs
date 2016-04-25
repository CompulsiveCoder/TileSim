using System;
using tilesim.Engine.Entities;
using tilesim.Engine.Needs;
using System.Collections.Generic;

namespace tilesim.Engine
{
	public abstract class BaseNeedIdentifier
	{
        public ActionType ActionType { get;set; }

		public ItemType ItemType { get; set; }

		public decimal DefaultPriority { get;set; }

		public EngineSettings Settings { get; set; }

        public List<NeedEntry> Needs = new List<NeedEntry>();

        public ConsoleHelper Console { get; set; }

        public BaseNeedIdentifier(ActionType actionType, ItemType needType, EngineSettings settings, ConsoleHelper console)
		{
            ActionType = actionType;
			ItemType = needType;
            DefaultPriority = settings.DefaultPriorities[needType];
			Settings = settings;
            Console = console;
		}

		public abstract bool IsNeeded (Person person);

        public abstract void RegisterNeed(Person person, ActionType actionType, ItemType needType, decimal priority);

		public virtual void RegisterIfNeeded(Person person)
		{            
            var priority = DefaultPriority;

            var needIsNotAlreadyRegistered = !NeedIsRegistered (person, ActionType, ItemType, priority);

            var requiresRegistration = (IsNeeded(person) && needIsNotAlreadyRegistered);

			if (requiresRegistration)
                RegisterNeed(person, ActionType, ItemType, priority);

            CommitNeeds (person);
		}

        public virtual bool NeedIsRegistered (Person person, ActionType actionType, ItemType needType, decimal quantity)
        {
            return person.HasNeed (actionType, needType, quantity);
        }

        public void AddNeed(ActionType actionType, ItemType needType, decimal quantity, decimal priority)
        {
            if (Settings.IsVerbose)
                Console.WriteDebugLine ("    Registering the need to " + actionType + " " + quantity + " " + needType + ".");

            Needs.Add (new NeedEntry (actionType, needType, quantity, priority));
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

