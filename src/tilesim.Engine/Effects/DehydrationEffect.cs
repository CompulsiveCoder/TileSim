using System;
using tilesim.Engine.Entities;
using tilesim.Engine.Activities;

namespace tilesim.Engine.Effects
{
    public class DehydrationEffect : BasePersonEffect
    {
        public DehydrationEffect (EngineSettings settings, ConsoleHelper console) : base(settings, console)
        {
        }

        public override bool IsApplicable (Person person)
        {
            var personIsDrinking = (person.ActivityText == typeof(DrinkWaterActivity).Name);
            var personIsDehydrated = person.Vitals [PersonVital.Thirst] > Settings.DehydrationThreshold;
            return person.IsAlive
                && personIsDehydrated
                && !personIsDrinking;
        }

        public override void Execute (Person person)
        {
            var difference = person.Vitals [PersonVital.Thirst] - Settings.DehydrationThreshold;

            VitalsChange.Add (PersonVital.Health, -difference);
        }
    }
}

