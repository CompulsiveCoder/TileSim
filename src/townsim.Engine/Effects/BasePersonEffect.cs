using System;
using System.Collections.Generic;
using townsim.Engine.Entities;

namespace townsim.Engine
{
    public abstract class BasePersonEffect : BaseEffect
    {
        public Dictionary<PersonVital, decimal> VitalsChange = new Dictionary<PersonVital, decimal> ();

        public BasePersonEffect (EngineSettings settings) : base(settings)
        {
        }

        public void Apply(Person person)
        {
            Execute (person);

            Finished (person);
        }

        public override void Execute ()
        {
            throw new NotSupportedException("Person object required as argument.");
        }

        public abstract void Execute (Person person);

        public override void Finished ()
        {
            throw new NotSupportedException("Person object required as argument.");
        }

        public void Finished (Person person)
        {
            CommitVitalsChanges (person);
        }

        public void CommitVitalsChanges(Person person)
        {
            // TODO: Merge this with the commit function from BaseActivity. Both commit vitals changes
            if (Settings.IsVerbose) {
                Console.WriteLine ("    Committing vitals changes");
            }

            foreach (var vital in VitalsChange.Keys) {
                var changeValue = VitalsChange [vital];
                var previousValue = person.Vitals [vital];
                var newValue = previousValue + changeValue;

                newValue = PercentageValidator.Validate (newValue);

                if (Settings.IsVerbose) {
                    Console.WriteLine ("      " + vital);
                    Console.WriteLine ("        Previous: " + previousValue);
                    Console.WriteLine ("        Change: " + changeValue);
                    Console.WriteLine ("        New value: " + newValue);
                }
                person.Vitals [vital] = newValue;
            }

            VitalsChange.Clear ();
        }
    }
}

