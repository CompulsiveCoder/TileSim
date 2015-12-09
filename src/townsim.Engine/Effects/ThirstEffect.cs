using System;
using townsim.Entities;
using townsim.Data;

namespace townsim.Engine.Effects
{
	public class ThirstEffect
	{
		public decimal ThirstRate = 0.3m;//100m / (24*60*60) * 5m; // 100% / seconds in a day * drinks per day

		public EngineSettings Settings { get;set; }

		public ThirstEffect (EngineSettings settings)
		{
			Settings = settings;
		}

		public void Update(Person person)
		{
			if (person.IsAlive)
			{
				UpdateThirst (person);
			}
		}

		public void UpdateThirst(Person person)
		{
			person.Thirst += ThirstRate;

			if (person.Thirst > 100) {
				person.Thirst = 100;
				//person.Priorities [PriorityTypes.Water] = (int)person.Thirst; // TODO: Clean up
			}
			// TODO: Clean up
			//else if (person.Thirst > 10)
			//	person.Priorities [PriorityTypes.Water] = person.Thirst;
			
		}
	}
}

