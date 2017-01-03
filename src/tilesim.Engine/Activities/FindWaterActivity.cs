using System;
using tilesim.Engine.Activities;
using tilesim.Engine.Entities;

namespace tilesim.Engine
{
    public class FindWaterActivity : BaseActivity
    {
        public FindWaterActivity (Person actor, NeedEntry needEntry, EngineSettings settings, ConsoleHelper console)
            : base(actor, needEntry, settings, console)
        {
        }

        public override void Prepare (Person person)
        {
            throw new NotImplementedException ();
        }

        public override void Execute (Person person)
        {
            Status = "Finding water";

            if (Settings.IsVerbose) {
                Console.WriteDebugLine ("Finding water");
                //Console.WriteDebugLine ("  Current thirst: " + person.Vitals[PersonVitalType.Thirst]); // TODO: Remove if not needed
            }

            throw new NotImplementedException ();
          /* var amount = Settings.DefaultFindAmount;

            if (amount > person.Inventory [ItemType.Water])
                amount = person.Inventory [ItemType.Water];

            if (Settings.IsVerbose)
                Console.WriteDebugLine ("  Amount: " + amount);

            ItemsConsumed[ItemType.Water] += amount;

            var thirstDecrease = amount * Settings.WaterForThirstRatio;

            if (Settings.IsVerbose)
                Console.WriteDebugLine ("  Decreased thirst by: " + thirstDecrease);

            VitalsChange.Add (PersonVitalType.Thirst, -thirstDecrease);

            TotalWaterConsumed += amount;*/
        }

        public override bool CheckFinished ()
        {

            throw new NotImplementedException ();
            //return TotalWaterConsumed >= NeedEntry.Quantity;
        }

        public override void ConfirmProduced (NeedEntry entry)
        {
            throw new NotImplementedException ();
        }

        public override bool IsActorAbleToAct (Person actor)
        {
            throw new NotImplementedException ();
        }

        public override void RegisterNeeds (Person actor)
        {
            throw new NotImplementedException ();
        }
    }
}

