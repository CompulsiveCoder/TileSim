using System;
using townsim.Data;
using townsim.Entities;
using System.Collections.Generic;

namespace townsim.Engine
{
	public class PopulationEngine
	{
		public double AgingRate = 0.1;

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
			var randomNumber = new Random (DateTime.Now.Millisecond).Next (1, 20);
			var numberOfBabies = town.Statistics.TotalBreedingPairs / randomNumber;

			/*if (town.Population > 100)
				amount = town.Population / 50;
			else
				amount += town.Population / 20;*/

			if (numberOfBabies > 0) {
				IncreasePopulation (town, new PersonCreator ().CreateBabies (numberOfBabies));

				town.TotalBirths += numberOfBabies;
			}
		}

		public void UpdatePopulationDeaths(Town town)
		{
			// General deaths
			//var amount = (town.Population / 50);
			//Die (town, amount);

			// Old age deaths
			foreach (var person in town.People) {
				var randomNumber = new Random ().Next (40, 200);
				if (person.Age > randomNumber)
					Die (town, person);
			}
		}

		public void UpdatePopulationMigration(Town town)
		{
			// Arriving
			var probability = new Random ().Next (100);
			if (probability > 90)
			{
				var value = new Random ().Next (3);
				Immigrate (town, value);
			}

			// Leaving
			var leavingProbability = new Random ().Next (100);
			if (leavingProbability < town.Statistics.TotalHomelessPeople) {
				var value = new Random ().Next (3);
				Emigrate (town, value);
			}
		}

		/*public void IncreasePopulation(Town town, int numberOfNewPeople)
		{
			var peopleEngine = new PeopleEngine ();
			var newPeople = peopleEngine.NewPeople (numberOfNewPeople);

			var people = new List<Person> (town.People);
			people.AddRange (newPeople);

			town.People = people.ToArray ();

			var saver = new PersonSaver ();
			for (int i = 0; i < newPeople.Length; i++)
				saver.Save (newPeople [i]);
			//for (int i = 0; i < numberOfNewPeople; i++) {
				//var person = n
			//}
		}*/


		public void IncreasePopulation(Town town, Person[] newPeople)
		{
			//var peopleEngine = new PeopleEngine ();
			//var newPeople = peopleEngine.NewPeople (numberOfNewPeople);

			var people = new List<Person> (town.People);
			people.AddRange (newPeople);

			town.People = people.ToArray ();

			var saver = new PersonSaver ();
			for (int i = 0; i < newPeople.Length; i++)
				saver.Save (newPeople [i]);
			/*for (int i = 0; i < numberOfNewPeople; i++) {
				var person = n
			}*/
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
			/*var peopleEngine = new PeopleEngine ();
			var people = peopleEngine.NewPeople (numberOfPeople);

			var saver = new PersonSaver ();
			for (int i = 0; i < people.Length; i++)
				saver.Save (people [i]);*/

			// TODO: Change this so instead of creating people it moves people from other towns
			IncreasePopulation (town, new PersonCreator().CreateAdults(numberOfPeople));

			town.TotalImmigrants += numberOfPeople;
		}

		public void Emigrate(Town town, int numberOfPeople)
		{
			ReducePopulation (town, numberOfPeople);

			town.TotalEmigrants += numberOfPeople;
			/*var personDeleter = new PersonDeleter ();
			var list = new List<Person> (town.People);
			for (int i = 0; i < numberOfPeople; i++)
			{
				list.RemoveAt (i);
				personDeleter.Delete (town.People [i].Id);
			}
			town.People = list.ToArray ();*/
		}
	}
}

