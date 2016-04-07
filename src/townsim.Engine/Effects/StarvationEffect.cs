using System;
using townsim.Engine.Entities;
using townsim.Data;
using townsim.Engine.Activities;

namespace townsim.Engine.Effects
{
    public class StarvationEffect : BasePersonEffect
    {
        public StarvationEffect (EngineSettings settings, ConsoleHelper console) : base(settings, console)
        {
        }

        public override bool IsApplicable (Person person)
        {
            var personIsEating = (person.ActivityName == typeof(EatFoodActivity).Name);
            var personIsStarving = person.Vitals [PersonVital.Hunger] > Settings.StarvationThreshold;
            return person.IsAlive
                && personIsStarving
                && !personIsEating;
        }

        public override void Execute (Person person)
        {
            var difference = person.Vitals [PersonVital.Hunger] - Settings.StarvationThreshold;

            VitalsChange.Add (PersonVital.Health, -difference);
        }
    }
}

