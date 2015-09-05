using System;

namespace townsim.Entities
{
	public class Plant
	{
		public double Age { get;set; }
		public double Size { get;set; }
		public PlantType Type { get;set; }

		public Plant ()
		{
		}

		public Plant(PlantType type, double size)
		{
			Type = type;
			Size = size;
		}
	}
}

