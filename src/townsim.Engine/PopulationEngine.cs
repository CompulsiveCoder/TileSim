using System;
using townsim.Data;
using townsim.Entities;
using System.Collections.Generic;

namespace townsim.Engine
{
	public class PopulationEngine
	{
		public double AgingRate = 0.1;
		public LogWriter Log = new LogWriter ();
    public int BirthOdds = 25; // 1 in 25

		public PopulationEngine ()
		{
		}

		public void Update(Town town)
		{
			UpdatePopulationAge (town);
			UpdatePopulationBirthRate (town);
			UpdatePopulationDeaths (town);
			UpdatePopulationMigration (town);
		}

		public void UpdatePopulationAge(Town town)
		{
			foreach (var person in town.People) {
				person.IncreaseAge (AgingRate);
			}
		}

		public void UpdatePopulationBirthRate(Town town)
		{
      var random = new Random ();
			for (int i = 0; i < town.TotalBreedingPairs; i++) {
        var randomNumber = random.Next (1, BirthOdds);
				if (randomNumber < 1) {
					IncreasePopulation (town, new PersonCreator ().CreateBabies (1));
					town.TotalBirths++;
					Log.AppendLine (CurrentEngine.Id, "A baby was born.");
				}
			}
		}

		public void UpdatePopulationDeaths(Town town)
		{
			// General deaths
			//var amount = (town.Population / 50);
			//Die (town, amount);

			// Old age deaths
			foreach (var person in town.People) {
				var randomNumber = new Random ().Next (40, 1000);
				if (person.Age > randomNumber)
					Die (town, person);
			}
		}

		public void UpdatePopulationMigration(Town town)
		{
			// Arriving
			var probability = new Random ().Next (100);
			if (probability < 2)
			{
				var value = new Random ().Next (3);
				if (value > 0)
					Immigrate (town, value);
			}

			// Leaving
			var leavingProbability = new Random ().Next (500);
			if (leavingProbability < town.TotalHomelessPeople) {
				var value = new Random ().Next (1, 3);
				Emigrate (town, value);
			}
		}

		public void IncreasePopulation(Town town, Person[] newPeople)
		{
			var people = new List<Person> (town.People);
			people.AddRange (newPeople);

			town.People = people.ToArray ();

			var saver = new PersonSaver ();
			for (int i = 0; i < newPeople.Length; i++)
				saver.Save (newPeople [i]);
		}

		public void ReducePopulation(Town town, int numberOfPeople)
		{
			var personDeleter = new PersonDeleter ();
			if (town.Population > 0) {
				if (town.Population < numberOfPeople)
					numberOfPeople = town.Population;
				
				var list = new List<Person> (town.People);
				for (int i = 0; i < numberOfPeople; i++) {
					if (list.Count > 0) {
						list.RemoveAt (0);
						personDeleter.Delete (town.People [i].Id);
					}
				}
				town.People = list.ToArray ();
			}
		}

		public void Die(Town town, int numberOfPeople)
		{
			ReducePopulation (town, numberOfPeople);
			town.TotalDeaths += numberOfPeople;
		}

		public void Die(Town town, Person person)
		{
			//ReducePopulation (town, numberOfPeople);
			town.TotalDeaths += 1;

			var personDeleter = new PersonDeleter ();
			personDeleter.Delete (person.Id);

			var list = new List<Person> (town.People);
			list.Remove (person);
			town.People = list.ToArray ();
		}

		public void Immigrate(Town town, int numberOfPeople)
		{
			// TODO: Change this so instead of creating people it moves people from other towns
			IncreasePopulation (town, new PersonCreator().CreateAdults(numberOfPeople));

			town.TotalImmigrants += numberOfPeople;

      var peopleWord = "people";
      if (numberOfPeople == 1)
        peopleWord = "person";

      LogWriter.Current.AppendLine (CurrentEngine.Id, numberOfPeople + " new " + peopleWord + " arrived in town.");
		}

		public void Emigrate(Town town, int numberOfPeople)
		{
			ReducePopulation (town, numberOfPeople);

			town.TotalEmigrants += numberOfPeople;

			var peopleWord = "people";
			if (numberOfPeople == 1)
				peopleWord = "person";

			LogWriter.Current.AppendLine (CurrentEngine.Id, numberOfPeople + " " + peopleWord + " left town.");
		}
	}
}

