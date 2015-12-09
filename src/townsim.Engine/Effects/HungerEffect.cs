using System;
using townsim.Entities;
using townsim.Data;

namespace townsim.Engine.Effects
{
	public class HungerEffect
	{
		public decimal HungerRate = 0.1m;//100m / (24*60*60) * 3m; // 100% / seconds in a day * meals per day

		public EngineSettings Settings { get;set; }

		public HungerEffect (EngineSettings settings)
		{
			Settings = settings;
		}

		public void Update(Person person)
		{
			if (person.IsAlive) {
				UpdateHunger (person);
			}
		}

		public void UpdateHunger(Person person)
		{
			var increase = HungerRate;

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

