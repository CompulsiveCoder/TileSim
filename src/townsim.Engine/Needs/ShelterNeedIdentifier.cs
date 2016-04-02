using System;
using townsim.Engine.Entities;

namespace townsim.Engine.Needs
{
	public class ShelterNeedIdentifier : BaseNeedIdentifier
	{
		public ShelterNeedIdentifier (EngineSettings settings)
			: base(ItemType.Shelter, 100, settings)
		{
		}

		public override bool IsNeeded (Person person)
		{
			return person.IsHomeless;
		}

		/*public override void IdentifyAndRegisterNeed(Person person)
		{
			if (Settings.IsVerbose)
				Console.WriteLine ("      Identifying the need for shelter.");

			var needType = NeedType.Shelter;
			var priority = 100;

			var needIsNotAlreadyRegistered = !NeedIsRegistered (person, needType, priority);

			var requiresRegistration = (person.IsHomeless && needIsNotAlreadyRegistered);

			if (requiresRegistration)
				RegisterNeed(person, needType, 1, priority);
		}*/

		public override void RegisterNeed(Person person, ItemType needType, decimal quantity, decimal priority)
		{
			if (!NeedIsRegistered (person, needType, quantity)) {
				person.AddNeed (needType, quantity, priority);
			}
		}

		public override bool NeedIsRegistered (Person person, ItemType needType, decimal quantity)
		{
			return person.HasNeed (needType, quantity);
		}
	}
}
