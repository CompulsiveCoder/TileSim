using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine.Activities
{
    [Serializable]
    [Activity(ActivityVerb.Sleep, ItemType.NotSet, PersonVitalType.Energy)]
    public class SleepActivity : BaseActivity
    {
        public decimal TotalEnergyRecovered = 0;

        public SleepActivity (Person person, NeedEntry needEntry, EngineSettings settings, ConsoleHelper console)
            : base(person, needEntry, settings, console)
        {
        }

        public override bool CheckFinished ()
        {
            return TotalEnergyRecovered >= NeedEntry.Quantity;
        }

        public override void Prepare (Person person)
        {
            throw new NotImplementedException ();
        }

        public override void Execute (Person person)
        {
            if (Settings.IsVerbose) {
                Console.WriteDebugLine ("Starting " + GetType ().Name + " activity");
                Console.WriteDebugLine ("  Quantity (energy): " + NeedEntry.Quantity);
            }

            var amountOfEnergyThisCycle = Settings.EnergyFromSleepRate;

            // If the person has no shelter then halve the amount of energy recovered
            if (!person.HasShelter)
                amountOfEnergyThisCycle = amountOfEnergyThisCycle / 2;

            TotalEnergyRecovered += amountOfEnergyThisCycle;

            VitalsChange.Add (PersonVitalType.Energy, amountOfEnergyThisCycle);

            var fraction = NeedEntry.Quantity / 100;

            PercentComplete = fraction * TotalEnergyRecovered;

            Status = String.Format ("Sleeping {0}%", PercentComplete);
        }

        public override bool CanAct(Person actor)
        {
            return true;
        }
    }
}

