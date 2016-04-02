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

		public BaseNeedIdentifier(ItemType needType, decimal defaultPriority, EngineSettings settings)
		{
			Type = needType;
			DefaultPriority = defaultPriority;
			Settings = settings;
		}

		public abstract bool IsNeeded (Person person);

		// TODO: Remove if not needed
		//public abstract NeedEntry IdentifyNeed(Person person);


		public abstract void RegisterNeed(Person person, ItemType needType, decimal quantity, decimal priority);

		public abstract bool NeedIsRegistered(Person person, ItemType needType, decimal quantity);

		public virtual void RegisterIfNeeded(Person person)
		{
			if (Settings.IsVerbose)
				Console.WriteLine ("      Identifying the need for shelter.");

			var needType = ItemType.Shelter;
			var priority = 100;

			var needIsNotAlreadyRegistered = !NeedIsRegistered (person, needType, priority);

			var requiresRegistration = (person.IsHomeless && needIsNotAlreadyRegistered);

			if (requiresRegistration)
				RegisterNeed(person, needType, 1, priority);
		}
	}
}

