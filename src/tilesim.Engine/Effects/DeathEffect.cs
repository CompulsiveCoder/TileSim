using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine
{
    public class DeathEffect : BasePersonEffect
    {
        public DeathEffect (EngineSettings settings, ConsoleHelper console) : base(settings, console)
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
            if (person.Id == Settings.PlayerId
                && !person.IsAlive)
                Console.
        }
    }
}

