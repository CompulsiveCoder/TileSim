using System;
using System.Collections.Generic;
using tilesim.Engine.Entities;

namespace tilesim.Engine
{
    public abstract class BasePersonEffect : BaseEffect
    {
        public Dictionary<PersonVitalType, decimal> VitalsChange = new Dictionary<PersonVitalType, decimal> ();

        public BasePersonEffect (EngineSettings settings, ConsoleHelper console)
            : base(settings, console)
        {
        }

        public override bool IsApplicable ()
        {
            throw new NotImplementedException ();
        }

        public abstract bool IsApplicable (Person person);

        public void Apply(Person person)
        {
            if (IsApplicable (person)) {
                Execute (person);

                Finished (person);
            }
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
                Console.WriteDebugLine ("    Committing vitals changes");
            }

            foreach (var vital in VitalsChange.Keys) {
                var changeValue = VitalsChange [vital];
                var previousValue = person.Vitals [vital];
                var newValue = previousValue + changeValue;

                newValue = PercentageValidator.Validate (newValue);

                if (Settings.IsVerbose) {
                    Console.WriteDebugLine ("      " + vital);
                    Console.WriteDebugLine ("        Previous: " + previousValue);
                    Console.WriteDebugLine ("        Change: " + changeValue);
                    Console.WriteDebugLine ("        New value: " + newValue);
                }
                person.Vitals [vital] = newValue;
            }

            VitalsChange.Clear ();
        }
    }
}

