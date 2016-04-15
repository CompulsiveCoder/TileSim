using System;
using townsim.Engine.Entities;

namespace townsim.Engine
{
    public class PlayerDeathEffect : BasePersonEffect
    {
        public PlayerDeathEffect (EngineSettings settings, ConsoleHelper console) : base(settings, console)
        {
        }

        public override bool IsApplicable (townsim.Engine.Entities.Person person)
        {
            var personIsPlayer = (Settings.PlayerId == person.Id);

            var personIsDead = person.Vitals [PersonVital.Health] <= 0;

            return personIsPlayer
                && personIsDead;
        }

        public override void Execute (townsim.Engine.Entities.Person person)
        {
            throw new PlayerDiedException (person);
        }
    }
}

