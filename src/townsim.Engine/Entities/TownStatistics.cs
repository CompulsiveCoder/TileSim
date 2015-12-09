using System;
using Newtonsoft.Json;
using System.Linq;

namespace townsim.Entities
{
	public partial class Town
	{

		[JsonIgnore]
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
		public int TotalCouples
		{
			get {
				int totalFertileMales = (from person in People
					where person != null
					&& person.Gender == Gender.Male
					&& person.IsAdult
					&& person.Age < 50 // Make this configurable
					select person).Count();

				int totalFertileFemales = (from person in People
					where person != null
					&& person.Gender == Gender.Male
					&& person.IsAdult
					&& person.Age < 50 // Make this configurable
					select person).Count();

				if (totalFertileMales > totalFertileFemales)
					return totalFertileFemales;
				else
					return totalFertileMales;
			}
		}

		[JsonIgnore]
		public int TotalMales
		{
			get {
				if (People == null || People.Length == 0)
					return 0;
				else
					return (from person in People
				        where person != null 
							&& person.Gender == Gender.Male
				        select person).Count ();
			}
		}

		[JsonIgnore]
		public int TotalFemales
		{
			get {
				if (People == null || People.Length == 0)
					return 0;
				else
					return (from person in People
						where person != null 
							&& person.Gender == Gender.Female
						select person).Count ();
			}
		}

		[JsonIgnore]
		public int TotalAdults
		{
			get {
				int totalAdults = 0;
				foreach (var person in People) {
					if (person.IsAdult) {
						totalAdults++;
					}
				}

				return totalAdults;
			}
		}

		[JsonIgnore]
		public int TotalChildren
		{
			get {
				int totalChildren = 0;
				foreach (var person in People) {
					if (person.IsChild) {
						totalChildren++;
					}
				}

				return totalChildren;
			}
		}

		[JsonIgnore]
		public double AverageAge
		{
			get {
				if (People.Length > 0) {
					double total = 0;

					foreach (var person in People) {
						total += person.Age;
					}

					return total / People.Length;
				} else
					return 0;
			}
		}

		[JsonIgnore]
		public int TotalBuilders
		{
			get {
				return (from person in People
				        where person != null
				            && person.ActivityType == ActivityType.Builder
				        select person).Count ();
			}
		}

		[JsonIgnore]
		public int TotalForestryWorkers
		{
			get {
				return (from person in People
					where person != null
					&& person.ActivityType == ActivityType.Forestry
					select person).Count ();
			}
		}

		[JsonIgnore]
		public int TotalActive
		{
			get {
				var total = (from person in People
						where person != null
							&& person.ActivityType != ActivityType.Inactive
					select person).Count();

				return total;
			}
		}

		[JsonIgnore]
		public int TotalInactive
		{
			get {
				var total = (from person in People
					where person != null
						&& person.ActivityType == ActivityType.Inactive
					select person).Count();

				return total;
			}
		}

		[JsonIgnore]
		public double AverageTreeSize
		{
			get {
				double sum = 0;
				var trees = Trees;
				foreach (var tree in trees) {
					sum += tree.Size;
				}
				return sum / trees.Length;
			}
		}

		[JsonIgnore]
		public double AverageTreeAge
		{
			get {
				double sum = 0;
				var trees = Trees;
				foreach (var tree in trees) {
					sum += tree.Age;
				}
				return sum / trees.Length;
			}
		}

		// TODO: Is this the best way to track this?
		public int CountTreesPlantedToday(TimeSpan gameTime)
		{
			int treesPlantedToday = 0;
			foreach (var plant in Plants) {
				if (plant.Type == PlantType.Tree
					&& plant.WasPlanted
					&& plant.TimePlanted.Days == gameTime.Days) {
					treesPlantedToday++;
				}
			}
			return treesPlantedToday;
		}

		public int TotalTreesBeingPlanted
		{
			get {
				int totalTreesBeingPlanted = 0;
				foreach (var plant in Plants) {
					if (plant.Type == PlantType.Tree
						&& plant.WasPlanted
					    && plant.PercentPlanted < 100) {
						totalTreesBeingPlanted++;
					}
				}
				return totalTreesBeingPlanted;
			}
		}

		// TODO: Is this the best way to track this?
		public int CountVegetablesPlantedToday(TimeSpan gameTime)
		{
			int vegetablesPlantedToday = 0;
			foreach (var plant in Plants) {
				if (plant.Type == PlantType.Vegetable
					&& plant.WasPlanted
					&& plant.TimePlanted.Days == gameTime.Days) {
					vegetablesPlantedToday++;
				}
			}
			return vegetablesPlantedToday;
		}

		public int TotalVegetablesBeingPlanted
		{
			get {
				int totalVegetablesBeingPlanted = 0;
				foreach (var plant in Plants) {
					if (plant.Type == PlantType.Vegetable
						&& plant.WasPlanted
						&& plant.PercentPlanted < 100) {
						totalVegetablesBeingPlanted++;
					}
				}
				return totalVegetablesBeingPlanted;
			}
		}

		[JsonIgnore]
		public double AverageVegetableSize
		{
			get {
				double sum = 0;
				var vegetables = Vegetables;
				foreach (var vegetable in vegetables) {
					sum += vegetable.Size;
				}
				return sum / vegetables.Length;
			}
		}

		[JsonIgnore]
		public double AverageVegetableAge
		{
			get {
				double sum = 0;
				var vegetables = Vegetables;
				foreach (var vegetable in vegetables) {
					sum += vegetable.Age;
				}
				return sum / vegetables.Length;
			}
		}

		// TODO: Is this the best way to track this?
		public int CountVegetablesHarvestedToday(TimeSpan gameTime)
		{
			int vegetablesHarvestedToday = 0;
			foreach (var plant in Plants) {
				if (plant.Type == PlantType.Vegetable
					&& plant.WasHarvested
					&& plant.TimeHarvested.Days == gameTime.Days) {
					vegetablesHarvestedToday++;
				}
			}
			return vegetablesHarvestedToday;
		}

		public int TotalVegetablesBeingHarvested
		{
			get {
				int totalVegetablesBeingHarvested = 0;
				foreach (var plant in Plants) {
					if (plant.Type == PlantType.Vegetable
						&& plant.WasHarvested
						&& plant.PercentHarvested < 100) {
						totalVegetablesBeingHarvested++;
					}
				}
				return totalVegetablesBeingHarvested;
			}
		}
	}
}

