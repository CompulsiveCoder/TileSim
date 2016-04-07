using System;
using townsim.Engine.Entities;
using townsim.Data;
using townsim.Engine.Activities;

namespace townsim.Engine.Effects
{
	public class HungerEffect : BasePersonEffect
	{
        public HungerEffect (EngineSettings settings, ConsoleHelper console) : base(settings, console)
		{
		}

        public override bool IsApplicable (Person person)
        {
            var personIsEating = (person.ActivityName == typeof(EatFoodActivity).Name);
            return person.IsAlive && !personIsEating;
        }

        public override void Execute (Person person)
        {
            VitalsChange.Add (PersonVital.Hunger, +Settings.HungerRate);
        }
	}
}

