using System;
using townsim.Engine.Entities;

namespace townsim.Engine.Needs
{
    public class WaterNeedIdentifier : BaseNeedIdentifier
    {
        public WaterNeedIdentifier (EngineSettings settings)
            : base(ItemType.Water, settings)
        {
        }

        public override bool IsNeeded (Person person)
        {
            return person.Thirst > Settings.ThirstThreshold;
        }

        public override void RegisterNeed(Person person, ItemType needType, decimal quantity, decimal priority)
        {
            if (!NeedIsRegistered (person, needType, quantity)) {
                person.AddNeed (needType, Settings.DefaultDrinkAmount, priority);
            }
        }
    }
}
