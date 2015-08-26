using System;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace townsim.Entities
{
	public class Town : BaseEntity
	{
		public string Name { get;set; }
		public int Population { get; set; }
		public double Forest { get;set; }
		//public double Timber { get;set; }
		public double WaterSources { get; set; }
		public double FoodSources { get; set; }
		public int Builders { get; set; }
		public int Workers { get;set; }
		public int WorkersAvailable {
			get {
				return Population - Workers;
			}
		}

		public int TotalHomelessPeople
		{
			get {
				if (Population <= Buildings.TotalCompletedHouses) {
					return 0;
				} else {
					return Population - Buildings.TotalCompletedHouses;
				}
			}
		}

		[JsonIgnore]
		public BuildingCollection Buildings { get; set; }

		public Town ()
		{
			Id = Guid.NewGuid ();
			Buildings = new BuildingCollection();
			InitializeDefaultValues ();
		}

		public Town (string name, int population)
		{
			Id = Guid.NewGuid ();
			Buildings = new BuildingCollection();
			Name = name;
			Population = population;
			InitializeDefaultValues ();
		}

		public void InitializeDefaultValues()
		{
			Population = 5;
			WaterSources = 10000;
			FoodSources = 5000;
			Forest = 30000;
		}

	}
}

