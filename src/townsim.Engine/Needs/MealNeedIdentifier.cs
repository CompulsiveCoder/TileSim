using System;
using townsim.Engine.Entities;

namespace townsim.Engine.Needs
{
    public class MealNeedIdentifier : BaseNeedIdentifier
    {
        public MealNeedIdentifier (EngineSettings settings)
            : base(ItemType.Meal, settings)
        {
        }

        public override bool IsNeeded (Person person)
        {
            return person.Vitals[PersonVital.Hunger] > Settings.HungerThreshold;
        }

        public override void RegisterNeed(Person person, ItemType needType, decimal quantity, decimal priority)
        {
            if (!NeedIsRegistered (person, needType, quantity)) {
                person.AddNeed (needType, Settings.DefaultEatAmount, priority);
            }
        }
    }
}
