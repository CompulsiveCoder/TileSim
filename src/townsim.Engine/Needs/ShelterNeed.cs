using System;
using townsim.Entities;

namespace townsim.Engine.Needs
{
	public class ShelterNeed : BaseNeed
	{
		public ShelterNeed (EngineSettings settings)
			: base(NeedType.Shelter, 100, settings)
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

		public override void RegisterNeed(Person person, NeedType needType, decimal quantity, decimal priority)
		{
			if (!NeedIsRegistered (person, needType, quantity)) {
				person.AddNeed (needType, quantity, priority);
			}
		}

		public override bool NeedIsRegistered (Person person, NeedType needType, decimal quantity)
		{
			return person.HasNeed (needType, quantity);
		}
	}
}
