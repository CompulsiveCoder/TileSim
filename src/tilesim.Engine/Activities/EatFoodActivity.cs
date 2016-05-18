using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine.Activities
{
    [Serializable]
    [Activity(ActionType.Eat, ItemType.Food)]
    public class EatFoodActivity : BaseActivity
    {
        public decimal CollectionRate = 50.0m;

        public decimal TotalfoodConsumed = 0;

        public EatFoodActivity (Person actor, NeedEntry needEntry, EngineSettings settings, ConsoleHelper console)
            : base(actor, needEntry, settings, console)
        {
        }

        public override void Prepare (Person person)
        {
            throw new NotImplementedException ();
        }

        public override void Execute (Person person)
        {
            Status = "Eating food";

            if (Settings.IsVerbose) {
                Console.WriteDebugLine ("Eating food");
                Console.WriteDebugLine ("  Current hunger: " + person.Vitals[PersonVital.Hunger]);
            }

            var amount = Settings.DefaultEatAmount;

            if (amount > person.Inventory [ItemType.Food])
                amount = person.Inventory [ItemType.Food];

            if (Settings.IsVerbose)
                Console.WriteDebugLine ("  Amount: " + amount);

            ItemsConsumed[ItemType.Food] += amount;

            var hungerDecrease = amount * Settings.FoodForHungerRatio;

            if (Settings.IsVerbose)
                Console.WriteDebugLine ("  Decreased hunger by: " + hungerDecrease);

            VitalsChange.Add (PersonVital.Hunger, -hungerDecrease);

            TotalfoodConsumed += amount;
        }

        public override bool CheckFinished ()
        {
            return TotalfoodConsumed >= NeedEntry.Quantity;
        }

        public override void ConfirmProduced (NeedEntry entry)
        {
            throw new NotImplementedException ();
        }

        public override bool CheckRequiredItems (Person actor)
        {
            var foodAvailable = actor.Inventory.Items [ItemType.Food] > 0;

            if (!foodAvailable && Settings.IsVerbose) {
                Console.WriteDebugLine ("    No food available.");
                RegisterNeedToGatherFood ();
            }

            return foodAvailable;
        }

        public void RegisterNeedToGatherFood()
        {
            AddNeed (ActionType.Gather, ItemType.Food, NeedEntry.Quantity, NeedEntry.Priority + 1);
        }
    }
}