using System;
using datamanager.Entities;
using tilesim.Engine;

namespace tilesim.Engine.Entities
{
	[Serializable]
	public class Plant// : IActivityTarget // TODO: Remove
	{
		public decimal Age { get;set; }
		public decimal Size { get;set; }
		public PlantType Type { get;set; }
		public bool WasPlanted { get;set; }
		public bool WasHarvested { get; set; }

		public TimeSpan TimePlanted { get;set; }

		public decimal PercentPlanted { get; set; }

		public decimal PercentHarvested { get; set; }

		public TimeSpan TimeHarvested { get; set; }

		public Person[] People { get;set; }

		public GameTile Tile { get;set; }

		public decimal TotalWood
		{
			get {
				if (Type == PlantType.Tree)
					return Size;
				else
					return 0;
			}
		}

		public Plant ()
		{
			People = new Person[]{ };
		}

		public Plant(PlantType type)
		{
			Type = type;

			Construct ();
		}

		public Plant(PlantType type, decimal age, decimal size)
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

