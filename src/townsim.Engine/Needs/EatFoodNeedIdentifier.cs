using System;
using townsim.Engine.Entities;

namespace townsim.Engine.Needs
{
    public class EatFoodNeedIdentifier : BaseNeedIdentifier
    {
        public EatFoodNeedIdentifier (EngineSettings settings, ConsoleHelper console)
            : base(ActionType.Eat, ItemType.Food, settings, console)
        {
        }

        public override bool IsNeeded (Person person)
        {
            return person.Vitals[PersonVital.Hunger] > Settings.HungerThreshold;
        }

        public override void RegisterNeed(Person person, ActionType actionType, ItemType needType, decimal priority)
        {
            var quantity = Settings.DefaultEatAmount;

            if (!NeedIsRegistered (person, actionType, needType, quantity)) {
                AddNeed (actionType, needType, quantity, priority);
            }
        }
    }
}
