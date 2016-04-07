using System;
using townsim.Engine.Entities;

namespace townsim.Engine.Needs
{
    public class BuildShelterNeedIdentifier : BaseNeedIdentifier
    {
        public BuildShelterNeedIdentifier (EngineSettings settings, ConsoleHelper console)
            : base(ActionType.Build, ItemType.Shelter, settings, console)
        {
        }

        public override bool IsNeeded (Person person)
        {
            return person.IsHomeless;
        }

        public override void RegisterNeed(Person person, ActionType actionType, ItemType needType, decimal priority)
        {
            if (!NeedIsRegistered (person, actionType, needType, 1)) {
                AddNeed (actionType, needType, 1, priority);
            }
        }
    }
}
