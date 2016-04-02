using System;
using townsim.Engine.Entities;
using townsim.Engine.Needs;

namespace townsim.Engine
{
	public abstract class BaseNeedIdentifier
	{
		public ItemType Type { get; set; }

		public decimal DefaultPriority { get;set; }

		public EngineSettings Settings { get; set; }

		public BaseNeedIdentifier(ItemType needType, EngineSettings settings)
		{
			Type = needType;
            DefaultPriority = settings.DefaultPriorities[needType];
			Settings = settings;
		}

		public abstract bool IsNeeded (Person person);

		public abstract void RegisterNeed(Person person, ItemType needType, decimal quantity, decimal priority);

		public virtual void RegisterIfNeeded(Person person)
		{
			if (Settings.IsVerbose)
				Console.WriteLine ("      Identifying the need for shelter.");
            
            var priority = DefaultPriority;

			var needIsNotAlreadyRegistered = !NeedIsRegistered (person, Type, priority);

            var requiresRegistration = (IsNeeded(person) && needIsNotAlreadyRegistered);

			if (requiresRegistration)
				RegisterNeed(person, Type, 1, priority);
		}

        public virtual bool NeedIsRegistered (Person person, ItemType needType, decimal quantity)
        {
            return person.HasNeed (needType, quantity);
        }
	}
}

