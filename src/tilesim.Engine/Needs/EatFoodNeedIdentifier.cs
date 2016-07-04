using System;
using tilesim.Engine.Entities;
using tilesim.Engine.Activities;

namespace tilesim.Engine.Needs
{
    public class EatFoodNeedIdentifier : BaseNeedIdentifier
    {
        public EatFoodNeedIdentifier (EngineSettings settings, ConsoleHelper console)
            : base(ActivityVerb.Eat, ItemType.Food, PersonVitalType.Hunger, settings, console)
        {
        }

        public override bool IsNeeded (Person person)
        {
            return person.Vitals[PersonVitalType.Hunger] > Settings.HungerThreshold;
        }

        public override void RegisterNeed(Person person, ActivityVerb actionType, ItemType needType, PersonVitalType vitalType, decimal priority)
        {
            var quantity = Settings.DefaultEatAmount;

            if (!NeedIsRegistered (person, actionType, needType, vitalType, quantity)) {
                AddNeed (actionType, needType, vitalType, quantity, priority);
            }
        }
    }
}
