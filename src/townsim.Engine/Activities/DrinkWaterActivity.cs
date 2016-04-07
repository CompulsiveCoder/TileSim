﻿using System;
using townsim.Engine.Entities;
using townsim.Data;

namespace townsim.Engine.Activities
{
    [Serializable]
    [Activity(ActionType.Drink, ItemType.Water)]
    public class DrinkWaterActivity : BaseActivity
    {
        public decimal CollectionRate = 50.0m;

        public decimal TotalWaterConsumed = 0;

        public DrinkWaterActivity (Person actor, NeedEntry needEntry, EngineSettings settings, ConsoleHelper console)
            : base(actor, needEntry, settings, console)
        {
        }

        public override void Prepare (Person person)
        {
            throw new NotImplementedException ();
        }

        public override void Execute (Person person)
        {
            if (Settings.IsVerbose) {
                Console.WriteDebugLine ("Drinking water");
                Console.WriteDebugLine ("  Current thirst: " + person.Vitals[PersonVital.Thirst]);
            }

            var amount = Settings.DefaultDrinkAmount;

            if (amount > person.Inventory [ItemType.Water])
                amount = person.Inventory [ItemType.Water];

            if (Settings.IsVerbose)
                Console.WriteDebugLine ("  Amount: " + amount);
            
            ItemsConsumed[ItemType.Water] += amount;

            var thirstDecrease = amount * Settings.WaterForThirstRatio;

            if (Settings.IsVerbose)
                Console.WriteDebugLine ("  Decreased thirst by: " + thirstDecrease);

            VitalsChange.Add (PersonVital.Thirst, -thirstDecrease);

            TotalWaterConsumed += amount;
        }

        public override bool CheckFinished ()
        {
            return TotalWaterConsumed >= NeedEntry.Quantity;
        }

        public override void ConfirmProduced (NeedEntry entry)
        {
            throw new NotImplementedException ();
        }

        public override bool CheckRequiredItems (Person actor)
        {
            var waterAvailable = actor.Inventory.Items [ItemType.Water] > 0;

            if (!waterAvailable && Settings.IsVerbose) {
                Console.WriteDebugLine ("    No water available.");
                RegisterNeedToCollectWater ();
            }

            return waterAvailable;
        }

        public void RegisterNeedToCollectWater()
        {
            AddNeed (ActionType.Gather, ItemType.Water, NeedEntry.Quantity, NeedEntry.Priority + 1);
        }
    }
}