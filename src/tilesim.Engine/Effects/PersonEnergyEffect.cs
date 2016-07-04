using System;
using tilesim.Engine.Entities;
using tilesim.Engine.Activities;

namespace tilesim.Engine.Effects
{
    public class PersonEnergyEffect : BasePersonEffect
    {
        public PersonEnergyEffect (EngineSettings settings, ConsoleHelper console) : base(settings, console)
        {
        }

        public override bool IsApplicable (Person person)
        {
            // TODO: Should energy also be consumed when the player is inactive?
            return person.IsAlive && person.IsActive && person.Activity.GetType().Name != typeof(SleepActivity).Name;
        }

        public override void Execute (Person person)
        {
            VitalsChange.Add (PersonVitalType.Energy, -Settings.PersonEnergyConsumptionRate);
        }
    }
}

