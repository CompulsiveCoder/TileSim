using System;
using townsim.Engine.Entities;
using townsim.Data;

namespace townsim.Engine.Effects
{
	public class HealthEffect : BaseEffect
	{
		public HealthEffect (EngineContext context) : base(context)
		{
		}

		public void Update(Person person)
		{
			var isHarmed = false;

			if (person.Thirst >= 100) {

				Context.Log.WriteLine ("The player is dying of thirst.");
				var damage = person.Thirst / 100;
				person.Health -= damage;
				isHarmed = true;
			}

			if (person.Hunger >= 100) {
				Context.Log.WriteLine ("The player is dying of hunger.");
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
				person.IsAlive = false;
		}
	}
}

