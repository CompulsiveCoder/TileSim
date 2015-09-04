using System;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using townsim.Alerts;

namespace townsim.Entities
{
	public partial class Town : BaseEntity
	{
		public string Name { get;set; }

		[JsonIgnore]
		public int Population
		{
			get { return People.Length; }
		}

		public int DefaultPopulation = 5;

		public double Forest { get;set; }
		public double WaterSources { get; set; }
		public double FoodSources { get; set; }

		public int TotalBirths { get;set; }
		public int TotalDeaths { get;set; }
		public int TotalImmigrants { get;set; }
		public int TotalEmigrants { get;set; }

		[JsonProperty]
		public Person[] People { get;set; }

		[JsonIgnore]
		public BuildingCollection Buildings { get; set; }

		[JsonIgnore]
		public BaseAlert[] Alerts { get; set; }

		public Town ()
		{
			Id = Guid.NewGuid ();
			InitializeDefaultValues (5);
		}

		public Town (int population)
		{
			Id = Guid.NewGuid ();
			InitializeDefaultValues (population);
		}

		public Town (string name, int population)
		{
			Id = Guid.NewGuid ();
			Name = name;
			InitializeDefaultValues (DefaultPopulation);
		}

		public void InitializeDefaultValues(int population)
		{
			WaterSources = 2500;
			FoodSources = 2500;
			Forest = 30000;

			Buildings = new BuildingCollection();

			var people = new PersonCollection ();
			var personCreator = new PersonCreator ();
			for (int i = 0; i < population; i++) {
				var person = personCreator.CreateAdult ();
				person.Location = this;
				people.Add (person);
			}
			People = people.ToArray ();
			Alerts = new BaseAlert[]{ };
		}

		public void AddAlert(BaseAlert alert)
		{
			var list = new List<BaseAlert> ();

			if (Alerts != null)
				list.AddRange (Alerts);

			if (!AlertExists(alert))
				list.Add (alert);

			Alerts = list.ToArray ();
		}

		public bool AlertExists(BaseAlert alert)
		{
			foreach (var a in Alerts)
				if (a.GetType () == alert.GetType ())
					return true;

			return false;
		}

		public void ValidateProperties()
		{
			if (WaterSources < 0)
				WaterSources = 0;
			if (FoodSources < 0)
				FoodSources = 0;
			if (Forest < 0)
				Forest = 0;
		}

	}
}

