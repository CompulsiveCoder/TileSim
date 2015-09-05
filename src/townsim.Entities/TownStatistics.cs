using System;
using Newtonsoft.Json;

namespace townsim.Entities
{
	public partial class Town
	{
		[JsonIgnore]
		public int TotalUnemployed
		{
			get {
				int totalUnemployed = 0;
				foreach (var person in People) {
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
				if (Population <= Buildings.TotalCompletedHouses) {
					return 0;
				} else {
					return Population - Buildings.TotalCompletedHouses;
				}
			}
		}

		[JsonIgnore]
		public int TotalBreedingPairs
		{
			get {
				int totalBreedingMales = 0;
				int totalBreedingFemales = 0;
				foreach (var person in People) {
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
				foreach (var person in People) {
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
				foreach (var person in People) {
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
				int totalBuilders = 0;
				foreach (var person in People) {
					if (person.EmploymentType == EmploymentType.Builder)
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
				foreach (var person in People) {
					if (person.IsEmployed)
						totalEmployed++;
				}
				return totalEmployed;
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
	}
}

