using System;
using tilesim.Engine.Entities;
using tilesim.Data;

namespace tilesim.Engine.Effects
{
	public class HealthEffect : BasePersonEffect
	{
        public HealthEffect (EngineSettings settings, ConsoleHelper console) : base(settings, console)
		{
		}

        public override bool IsApplicable (Person person)
        {
            return person.IsAlive;
        }

        public override void Execute (Person person)
        {
            var amountOfHarm = 0;

            amountOfHarm += CalculateHarmFromDehydration


            VitalsChange.Add (PersonVital.Hunger, -amountOfHarm);
        }

		public void Update(Person person)
		{
			/*var isHarmed = false;

            if (person.Vitals[PersonVital.Thirst] >= 100) {
                // TODO: Reimplement
				//Context.Log.WriteLine ("The player is dying of thirst.");
                var damage = person.Vitals[PersonVital.Thirst] / 100;
                person.Vitals[PersonVital.Health] -= damage;
				isHarmed = true;
			}

			if (person.Hunger >= 100) {
                // TODO: Reimplement
				//Context.Log.WriteLine ("The player is dying of hunger.");
				var damage = person.Hunger / 100;
				person.Health -= damage;
				isHarmed = true;
			}

			if (!isHarmed)
				person.Health += 5;

			if (person.Health < 0)
				person.Health = 0;

			if (person.Health > 100)
				person.Health = 100;

			if (person.Health == 0)
				person.IsAlive = false;*/
		}
	}
}

