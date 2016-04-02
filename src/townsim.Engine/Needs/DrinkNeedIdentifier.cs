using System;
using townsim.Engine.Entities;

namespace townsim.Engine.Needs
{
    public class DrinkNeedIdentifier : BaseNeedIdentifier
    {
        public DrinkNeedIdentifier (EngineSettings settings)
            : base(ItemType.Drink, settings)
        {
        }

        public override bool IsNeeded (Person person)
        {
            return person.Vitals[PersonVital.Thirst] > Settings.ThirstThreshold;
        }

        public override void RegisterNeed(Person person, ItemType needType, decimal quantity, decimal priority)
        {
            if (!NeedIsRegistered (person, needType, quantity)) {
                person.AddNeed (needType, Settings.DefaultDrinkAmount, priority);
            }
        }
    }
}
