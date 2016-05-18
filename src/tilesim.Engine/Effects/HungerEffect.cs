using System;
using tilesim.Engine.Entities;
using tilesim.Engine.Activities;

namespace tilesim.Engine.Effects
{
	public class HungerEffect : BasePersonEffect
	{
        public HungerEffect (EngineSettings settings, ConsoleHelper console) : base(settings, console)
		{
		}

        public override bool IsApplicable (Person person)
        {
            var personIsEating = (person.ActivityText == typeof(EatFoodActivity).Name);
            return person.IsAlive && !personIsEating;
        }

        public override void Execute (Person person)
        {
            VitalsChange.Add (PersonVital.Hunger, +Settings.HungerRate);
        }
	}
}

