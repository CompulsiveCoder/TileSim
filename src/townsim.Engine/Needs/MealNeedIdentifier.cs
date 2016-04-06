﻿using System;
using townsim.Engine.Entities;

namespace townsim.Engine.Needs
{
    public class MealNeedIdentifier : BaseNeedIdentifier
    {
        public MealNeedIdentifier (EngineSettings settings)
            : base(ActionType.Eat, ItemType.Food, settings)
        {
        }

        public override bool IsNeeded (Person person)
        {
            return person.Vitals[PersonVital.Hunger] > Settings.HungerThreshold;
        }

        public override void RegisterNeed(Person person, ActionType actionType, ItemType needType, decimal quantity, decimal priority)
        {
            if (!NeedIsRegistered (person, actionType, needType, quantity)) {
                AddNeed (actionType, needType, Settings.DefaultEatAmount, priority);
            }
        }
    }
}
