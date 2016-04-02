using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using townsim.Alerts;
using System.Linq;
using datamanager.Entities;
using townsim.Engine;

namespace townsim.Engine.Entities
{
	[Serializable]
	[JsonObject(IsReference = true)]
	public partial class Town : BaseGameEntity
	{
		public string Name { get;set; }

		[JsonIgnore]
		public int Population
		{
			get { return People.Length; }
		}

		static public int DefaultPopulation = 1;

		public double Timber = 0;

		public Plant[] Forest
		{
			get {
				return (from plant in Plants
				        where plant.Type == PlantType.Tree
				        select plant).ToArray ();
			}
		}

		public decimal WaterSources { get; set; }
		public decimal FoodSources { get; set; }

		public int TotalBirths { get;set; }
		public int TotalDeaths { get;set; }
		public int TotalImmigrants { get;set; }
		public int TotalEmigrants { get;set; }

		private Person[] people;
		[JsonProperty]
		[TwoWay("Town")]
		public Person[] People
		{
			get
			{
				return people;
			}
			set
			{
				// TODO: Find a better way to ensure no nulls are in the list
				var nullFound = false;
				var list = new List<Person> ();
				foreach (var p in value) {
					if (p == null)
						nullFound = true;
					else
						list.Add (p);
				}
				people = list.ToArray();
			}
		}

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

		[JsonIgnore]
		public Plant[] Vegetables
		{
			get{
				var list = new List<Plant> ();

				foreach (var plant in Plants)
					if (plant.Type == PlantType.Vegetable)
						list.Add (plant);

				return list.ToArray ();
			}
		}

		public int TreesToPlantPerDay { get;set; }
		public int TotalTreesPlanted { get;set; }

		public int VegetablesToPlantPerDay {
			get;
			set;
		}

		[JsonIgnore]
		public int TotalGardeners {
			get {
				return PeopleDoing(ActivityType.Gardening);
			}
		}

		public int TotalVegetablesPlanted {
			get;
			set;
		}

		public int VegetablesToHarvestPerDay {
			get;
			set;
		}

		public int TotalVegetablesHarvested {
			get;
			set;
		}

		public Plant[] RipeVegetables {
			get {
				return (from vegetable in Vegetables
				       where vegetable.Size > 50
					select vegetable).ToArray();
			}
		}

		[JsonIgnore]
		[TwoWay("Town")]
		public Building[] Buildings { get; set; }

		[JsonIgnore]
		public BaseAlert[] Alerts { get; set; }

		public Town ()
		{
			Construct ();
		}

		public Town (string name)
		{
			Name = name;
			Construct ();
		}

		public void Construct()
		{
			Id = Guid.NewGuid ().ToString();
			Buildings = new Building[]{};
		}

		/*public Town (params Person[] people)
		{
			InitializeDefaultValues (people);
		}

		public Town (int population)
		{
			// TODO: Remove
			//Id = Guid.NewGuid ();
			InitializeDefaultValues (population);
		}

		public Town (int population, int numberOfTrees)
		{
			// TODO: Remove
			//Id = Guid.NewGuid ();
			InitializeDefaultValues (population, numberOfTrees);
		}

		public Town (string name, int population)
		{
			// TODO: Remove
			//Id = Guid.NewGuid ();
			Name = name;
			InitializeDefaultValues (population);
		}*/

		public void InitializeDefaultValues()
		{
			throw new NotImplementedException ();
			//InitializeDefaultValues (
			//	DefaultPopulation,
			//	100
			//);
		}

		// TODO: Tidy up the following functions and remove unneeded ones
		public void InitializeDefaultValues(int population)
		{
			throw new NotImplementedException ();
			//InitializeDefaultValues (population, new Random().Next(500));
		}

		public void InitializeDefaultValues(int population, int numberOfTrees)
		{
			throw new NotImplementedException ();
			//var people = CreatePeople (population);
			//InitializeDefaultValues (people);
		}

		public void InitializeDefaultValues(Person[] people)
		{
			throw new NotImplementedException ();
			//InitializeDefaultValues (people, new Random().Next(500));
		}

		public void InitializeDefaultValues(Person[] people, int numberOfTrees)
		{
			/*People = people;
			foreach (var person in people) {
				person.Town = this;
			}*/

			//var random = new Random ();
			//WaterSources = random.Next(50000);
			//FoodSources = random.Next(1000);
			//Timber = 1000;
			//Forest = 25000;

			// TODO: Remove if not needed
			/*TreesToPlantPerDay = random.Next(5);

			VegetablesToPlantPerDay = random.Next(20);*/

			//CreateTrees (numberOfTrees);

			//Alerts = new BaseAlert[]{ };
		}

		public void Populate(TownPopulator populator)
		{
			//var populator = new TownPopulator (this, Context);
			
			//AddPeople(CreatePeople (numberOfPeople));

			populator.Town = this;

			populator.Populate ();
		}

		/*public Person[] CreatePeople(int numberOfPeople)
		{
			var people = new PersonCollection ();
			var personCreator = new PersonCreator ();
			for (int i = 0; i < numberOfPeople; i++) {
				var person = personCreator.CreateAdult ();
				person.Location = this;
				people.Add (person);
			}
			return people.ToArray ();
		}*/


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
				if (!person.IsActive) {
					list.Add (person);
				}
			}
			return list.ToArray ();
		}

		public int PeopleDoing(ActivityType activity)
		{

			throw new NotImplementedException ();
			/*var matchingPeople = (from person in People
			                      where
			                          person != null
			                          && person.ActivityType == activity
			                      select person).ToArray ();

			return matchingPeople.Length;*/
		}

		public Plant FindRipeUnassignedVegetable ()
		{
			var plants = (from vegetable in RipeVegetables
			        where vegetable.People.Length == 0
				select vegetable).ToArray();

			return plants.Length > 0
				? plants [0]
					: null;
		}

		public void AddPeople(Person[] newPeople)
		{
			var list = new List<Person> ();

			if (People != null)
				list.AddRange (People);

			list.AddRange (newPeople);

			foreach (var person in newPeople)
				person.Town = this;

			People = list.ToArray ();
		}
	}
}

