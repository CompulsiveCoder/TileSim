using System;
using townsim.Engine.Entities;

namespace townsim.Engine.Needs
{
    public class BuildShelterNeedIdentifier : BaseNeedIdentifier
    {
        public BuildShelterNeedIdentifier (EngineSettings settings)
            : base(ActionType.Build, ItemType.Shelter, settings)
        {
        }

        public override bool IsNeeded (Person person)
        {
            return person.IsHomeless;
        }

        public override void RegisterNeed(Person person, ActionType actionType, ItemType needType, decimal quantity, decimal priority)
        {
            if (!NeedIsRegistered (person, actionType, needType, quantity)) {
                AddNeed (actionType, needType, quantity, priority);
            }
        }
    }
}
