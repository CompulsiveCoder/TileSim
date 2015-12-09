using System;
using townsim.Entities;
using townsim.Data;

namespace townsim.Engine.Effects
{
	public class ThirstEffect
	{
	/*	public decimal WaterConsumptionRate = 0.3m; // liters
		public decimal ThirstSatisfactionRate = 1; // The rate at which thirst is reduced
*/
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
				person.Priorities [PriorityTypes.Water] = (int)person.Thirst;
			}
			else if (person.Thirst > 10)
				person.Priorities [PriorityTypes.Water] = person.Thirst;
			
		}
	}
}

