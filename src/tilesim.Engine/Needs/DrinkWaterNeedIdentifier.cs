using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine.Needs
{
    public class DrinkWaterNeedIdentifier : BaseNeedIdentifier
    {
        public DrinkWaterNeedIdentifier (EngineSettings settings, ConsoleHelper console)
            : base(ActionType.Drink, ItemType.Water, settings, console)
        {
        }

        public override bool IsNeeded (Person person)
        {
            return person.Vitals[PersonVital.Thirst] > Settings.ThirstThreshold;
        }

        public override void RegisterNeed(Person person, ActionType actionType, ItemType itemType, decimal priority)
        {
            var quantity = Settings.DefaultDrinkAmount;

            if (!NeedIsRegistered (person, actionType, itemType, quantity)) {
                AddNeed (actionType, itemType, quantity, priority);
            }
        }
    }
}
