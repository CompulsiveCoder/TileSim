using System;

namespace townsim.Entities
{
	public class Plant : IEmploymentTarget
	{
		public double Age { get;set; }
		public double Size { get;set; }
		public PlantType Type { get;set; }
		public bool WasPlanted { get;set; }

		public TimeSpan TimePlanted { get;set; }

		public double PercentPlanted { get; set; }

		public Person[] Workers { get;set; }

		public Plant ()
		{
			Workers = new Person[]{ };
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
			Workers = new Person[]{ };
		}
	}
}

