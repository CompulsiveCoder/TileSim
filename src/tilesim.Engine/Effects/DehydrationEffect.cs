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
            var personIsDehydrated = person.Vitals [PersonVitalType.Thirst] > Settings.DehydrationThreshold;
            return person.IsAlive
                && personIsDehydrated
                && !personIsDrinking;
        }

        public override void Execute (Person person)
        {
            var difference = person.Vitals [PersonVitalType.Thirst] - Settings.DehydrationThreshold;

            VitalsChange.Add (PersonVitalType.Health, -difference);
        }
    }
}

