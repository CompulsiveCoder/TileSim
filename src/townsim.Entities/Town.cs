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

		public int DefaultPopulation = 10;

		public double Timber = 0;

		public Plant[] Forest
		{
			get {
				var list = new List<Plant> ();
				foreach (var plant in Plants) {
					if (plant.Type == PlantType.Tree) {
						list.Add (plant);
					}
				}
				return list.ToArray ();
			}
		}

		public double WaterSources { get; set; }
		public double FoodSources { get; set; }

		public int TotalBirths { get;set; }
		public int TotalDeaths { get;set; }
		public int TotalImmigrants { get;set; }
		public int TotalEmigrants { get;set; }

		[JsonProperty]
		public Person[] People = new Person[]{};

		[JsonProperty]
		public Plant[] Plants = new Plant[]{};

		[JsonIgnore]
		public Plant[] Trees
		{
			get{
				var list = new List<Plant> ();

				foreach (var plant in Plants)
					if (plant.Type == PlantType.Tree)
						list.Add (plant);

				return list.ToArray ();
			}
		}

		public int TreesToPlantPerDay { get;set; }
		public int TotalTreesPlanted { get;set; }

		[JsonIgnore]
		public BuildingCollection Buildings { get; set; }

		[JsonIgnore]
		public BaseAlert[] Alerts { get; set; }

		public Town ()
		{
			Id = Guid.NewGuid ();
			InitializeDefaultValues ();
		}

		public Town (int population)
		{
			Id = Guid.NewGuid ();
			InitializeDefaultValues (population);
		}

		public Town (int population, int numberOfTrees)
		{
			Id = Guid.NewGuid ();
			InitializeDefaultValues (population, numberOfTrees);
		}

		public Town (string name, int population)
		{
			Id = Guid.NewGuid ();
			Name = name;
			InitializeDefaultValues (population);
		}

		public void InitializeDefaultValues()
		{
			InitializeDefaultValues (
				DefaultPopulation,
				100
			);
		}

		public void InitializeDefaultValues(int population)
		{
			InitializeDefaultValues (population, 100);
		}

		public void InitializeDefaultValues(int population, int numberOfTrees)
		{
			WaterSources = 25000;
			FoodSources = 25000;
			//Timber = 1000;
			//Forest = 25000;

			TreesToPlantPerDay = 1;

			Buildings = new BuildingCollection();

			CreatePeople (population);

			CreateTrees (numberOfTrees);

			Alerts = new BaseAlert[]{ };
		}

		public void CreatePeople(int numberOfPeople)
		{
			var people = new PersonCollection ();
			var personCreator = new PersonCreator ();
			for (int i = 0; i < numberOfPeople; i++) {
				var person = personCreator.CreateAdult ();
				person.Location = this;
				people.Add (person);
			}
			People = people.ToArray ();
		}

		public void CreateTrees(int numberOfTrees)
		{
			var list = new List<Plant> ();
			for (int i = 0; i < numberOfTrees; i++) {
				var tree = new Plant(PlantType.Tree, 100, 100);
				tree.WasPlanted = false;
				tree.PercentPlanted = 100; // TODO: Is this necessary?
				list.Add (tree);
			}
			Plants = list.ToArray ();
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
			//if (Forest < 0)
			//	Forest = 0;
		}

		public Person[] GetWorkers(int numberOfWorkers)
		{
			var list = new List<Person> ();
			foreach (var person in People) {
				if (!person.IsEmployed) {
					list.Add (person);
				}
			}
			return list.ToArray ();
		}

	}
}

