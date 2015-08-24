using System;

namespace townsim.Data
{
	public class Town : BaseEntity
	{
		public string Name { get;set; }
		public int Population { get; set; }
		public int Forest { get;set; }
		public int WaterSources { get; set; }

		public Town ()
		{
			Id = Guid.NewGuid ();
		}

		public Town (string name, int population)
		{
			Id = Guid.NewGuid ();
			Name = name;
			Population = population;
		}
	}
}

