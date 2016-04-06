using System;
using townsim.Engine.Entities;

namespace townsim.Engine.Needs
{
    public class DrinkNeedIdentifier : BaseNeedIdentifier
    {
        public DrinkNeedIdentifier (EngineSettings settings)
            : base(ActionType.Drink, ItemType.Water, settings)
        {
        }

        public override bool IsNeeded (Person person)
        {
            return person.Vitals[PersonVital.Thirst] > Settings.ThirstThreshold;
        }

        public override void RegisterNeed(Person person, ActionType actionType, ItemType itemType, decimal quantity, decimal priority)
        {
            if (!NeedIsRegistered (person, actionType, itemType, quantity)) {
                AddNeed (actionType, itemType, Settings.DefaultDrinkAmount, priority);
            }
        }
    }
}
