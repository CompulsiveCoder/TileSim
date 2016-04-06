using System;
using townsim.Engine.Entities;
using townsim.Data;

namespace townsim.Engine.Activities
{
    [Serializable]
    [Activity(ItemType.Meal)]
    public class EatMealActivity : BaseActivity
    {
        public decimal CollectionRate = 50.0m;

        public decimal TotalfoodConsumed = 0;

        public EatMealActivity (Person actor, NeedEntry needEntry, EngineSettings settings) : base(actor, needEntry, settings)
        {
        }

        public override void Prepare (Person person)
        {
            throw new NotImplementedException ();
        }

        public override void Execute (Person person)
        {
            if (Settings.IsVerbose) {
                Console.WriteLine ("Eating food");
                Console.WriteLine ("  Current hunger: " + person.Vitals[PersonVital.Hunger]);
            }

            var amount = Settings.DefaultEatAmount;

            if (amount > person.Inventory [ItemType.Food])
                amount = person.Inventory [ItemType.Food];

            if (Settings.IsVerbose)
                Console.WriteLine ("  Amount: " + amount);

            ItemsConsumed[ItemType.Food] += amount;

            var hungerDecrease = amount * Settings.FoodForHungerRatio;

            if (Settings.IsVerbose)
                Console.WriteLine ("  Decreased hunger by: " + hungerDecrease);

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
                Console.WriteLine ("    No food available.");
                RegisterNeedForfood ();
            }

            return foodAvailable;
        }

        public void RegisterNeedForfood()
        {
            AddNeed (ItemType.Food, NeedEntry.Quantity, NeedEntry.Priority + 1);
        }
    }
}