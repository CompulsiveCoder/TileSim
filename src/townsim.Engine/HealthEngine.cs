using System;
using townsim.Entities;
using townsim.Data;

namespace townsim.Engine
{
	public class HealthEngine
	{
		public HealthEngine ()
		{
		}

		public void Update(Person person)
		{
			var isHarmed = false;

			if (person.Thirst >= 100) {

				LogWriter.Current.AppendLine (CurrentEngine.Id, "The player is dying of thirst.");
				var damage = person.Thirst / 100;
				person.Health -= damage;
				isHarmed = true;
			}

			if (person.Hunger >= 100) {
				LogWriter.Current.AppendLine (CurrentEngine.Id, "The player is dying of hunger.");
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

