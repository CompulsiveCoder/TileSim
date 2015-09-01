using System;
using Newtonsoft.Json;

namespace townsim.Entities
{
	public class TownStatistics
	{
		public Town Town { get;set; }


		[JsonIgnore]
		public int TotalUnemployed
		{
			get {
				int totalUnemployed = 0;
				foreach (var person in Town.People) {
					if (!person.IsEmployed)
						totalUnemployed++;
				}
				return totalUnemployed;
			}
		}

		[JsonIgnore]
		public int TotalHomelessPeople
		{
			get {
				if (Town.Population <= Town.Buildings.TotalCompletedHouses) {
					return 0;
				} else {
					return Town.Population - Town.Buildings.TotalCompletedHouses;
				}
			}
		}

		[JsonIgnore]
		public int TotalBreedingPairs
		{
			get {
				int totalBreedingMales = 0;
				int totalBreedingFemales = 0;
				foreach (var person in Town.People) {
					if (person.IsAdult && person.Age <= 50) {
						if (person.Gender == Gender.Male)
							totalBreedingMales++;
						else
							totalBreedingFemales++;
					}
				}

				if (totalBreedingMales > totalBreedingFemales)
					return totalBreedingFemales;
				else
					return totalBreedingMales;
			}
		}

		[JsonIgnore]
		public int TotalMales
		{
			get {
				int totalMales = 0;
				foreach (var person in Town.People) {
					if (person.Gender == Gender.Male) {
						totalMales++;
					}
				}

				return totalMales;
			}
		}

		[JsonIgnore]
		public int TotalFemales
		{
			get {
				int totalFemales = 0;
				foreach (var person in Town.People) {
					if (person.Gender == Gender.Female) {
						totalFemales++;
					}
				}

				return totalFemales;
			}
		}

		[JsonIgnore]
		public int TotalAdults
		{
			get {
				int totalAdults = 0;
				foreach (var person in Town.People) {
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
				foreach (var person in Town.People) {
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
				if (Town.People.Length > 0) {
					double total = 0;

					foreach (var person in Town.People) {
						total += person.Age;
					}

					return total / Town.People.Length;
				} else
					return 0;
			}
		}

		[JsonIgnore]
		public int TotalBuilders
		{
			get {
				int totalBuilders = 0;
				foreach (var person in Town.People) {
					if (person.Employment == EmploymentType.Builder)
						totalBuilders++;
				}
				return totalBuilders;
			}

		}

		[JsonIgnore]
		public int TotalEmployed
		{
			get {
				int totalEmployed = 0;
				foreach (var person in Town.People) {
					if (person.IsEmployed)
						totalEmployed++;
				}
				return totalEmployed;
			}
		}

		public TownStatistics (Town town)
		{
			Town = town;
		}
	}
}

