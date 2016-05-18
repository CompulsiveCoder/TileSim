using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine.Needs
{
    public class EatFoodNeedIdentifier : BaseNeedIdentifier
    {
        public EatFoodNeedIdentifier (EngineSettings settings, ConsoleHelper console)
            : base(ActionType.Eat, ItemType.Food, PersonVitalType.Hunger, settings, console)
        {
        }

        public override bool IsNeeded (Person person)
        {
            return person.Vitals[PersonVitalType.Hunger] > Settings.HungerThreshold;
        }

        public override void RegisterNeed(Person person, ActionType actionType, ItemType needType, PersonVitalType vitalType, decimal priority)
        {
            var quantity = Settings.DefaultEatAmount;

            if (!NeedIsRegistered (person, actionType, needType, vitalType, quantity)) {
                AddNeed (actionType, needType, vitalType, quantity, priority);
            }
        }
    }
}
