using System;
using townsim.Entities;
using townsim.Data;

namespace townsim.Engine.Effects
{
	public class HungerEffect : BaseEffect
	{
		public HungerEffect (EngineContext context) : base(context)
		{
		}

		public void Update(Person person)
		{
			if (person.IsAlive) {
				UpdateHunger (person);
			}
		}

		public void UpdateHunger(Person person)
		{
			var increase = Context.Settings.HungerRate;

			person.Hunger += increase;

			// TODO: Clean up
			if (person.Hunger > 100) {
				person.Hunger = 100;
				//person.Priorities [PriorityTypes.Food] = person.Hunger;
			} //else if (person.Hunger > 10)
				//person.Priorities [PriorityTypes.Food] = person.Hunger;
			//else
				//erson.Priorities [PriorityTypes.Food] = new Random ().Next (20, 80);
			
		}
	}
}

