using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine
{
    public class PlayerDeathEffect : BasePersonEffect
    {
        public PlayerDeathEffect (EngineSettings settings, ConsoleHelper console) : base(settings, console)
        {
        }

        public override bool IsApplicable (tilesim.Engine.Entities.Person person)
        {
            var personIsPlayer = (Settings.PlayerId == person.Id);

            var personIsDead = person.Vitals [PersonVital.Health] <= 0;

            return personIsPlayer
                && personIsDead;
        }

        public override void Execute (tilesim.Engine.Entities.Person person)
        {
            throw new PlayerDiedException (person);
        }
    }
}

