using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine.Needs
{
    public class BuildShelterNeedIdentifier : BaseNeedIdentifier
    {
        public BuildShelterNeedIdentifier (EngineSettings settings, ConsoleHelper console)
            : base(ActivityType.Build, ItemType.Shelter, PersonVitalType.NotSet, settings, console)
        {
        }

        public override bool IsNeeded (Person person)
        {
            return !person.HasShelter;
        }

        public override void RegisterNeed(Person person, ActivityType actionType, ItemType itemType, PersonVitalType vitalType, decimal priority)
        {
            if (!NeedIsRegistered (person, actionType, itemType, vitalType, 1)) {
                AddNeed (actionType, itemType, vitalType, 1, priority);
            }
        }
    }
}
