using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class HealthEngine
	{
		public HealthEngine ()
		{
		}

		public void Update(Person person)
		{
			if (person.Thirst >= 100) {
				var damage = person.Thirst - 100;
				person.Health -= damage;
			}

			if (person.Hunger >= 100) {
				var damage = person.Hunger - 100;
				person.Health -= damage;
			}

			if (person.Health < 0)
				person.Health = 0;

			if (person.Health == 0)
				person.IsAlive = false;
		}
	}
}

