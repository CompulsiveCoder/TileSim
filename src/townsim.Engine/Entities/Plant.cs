using System;
using datamanager.Entities;

namespace townsim.Entities
{
	[Serializable]
	public class Plant : IActivityTarget
	{
		public double Age { get;set; }
		public double Size { get;set; }
		public PlantType Type { get;set; }
		public bool WasPlanted { get;set; }
		public bool WasHarvested { get; set; }

		public TimeSpan TimePlanted { get;set; }

		public double PercentPlanted { get; set; }

		public double PercentHarvested { get; set; }

		public TimeSpan TimeHarvested { get; set; }

		public Person[] People { get;set; }

		public Plant ()
		{
			People = new Person[]{ };
		}

		public Plant(PlantType type)
		{
			Type = type;

			Construct ();
		}

		public Plant(PlantType type, double age, double size)
		{
			Type = type;
			Size = size;
			Age = age;

			Construct ();
		}

		public void Construct()
		{
			People = new Person[]{ };
		}
	}
}

