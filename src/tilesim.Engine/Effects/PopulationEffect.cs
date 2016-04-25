using System;
using tilesim.Data;
using tilesim.Engine.Entities;
using System.Collections.Generic;
using datamanager.Data;

namespace tilesim.Engine.Effects
{
	public class PopulationEffect : BaseEffect
	{
		// TODO: Split up into multiple effects and/or activities


		public PopulationEffect (EngineContext context) : base(context)
		{
		}

		public void Update(Tile tile)
		{
			UpdatePopulationAge (tile);
			UpdatePopulationBirthRate (tile);
			UpdatePopulationMigration (tile);
		}

		public void UpdatePopulationAge(Tile tile)
		{
			foreach (var person in tile.People) {
				person.IncreaseAge (Context.Settings.AgingRate);
			}
		}

		public void UpdatePopulationBirthRate(Tile tile)
		{
			var random = new Random ();
			for (int i = 0; i < tile.TotalCouples; i++) {
				var randomNumber = random.Next (1, Context.Settings.BirthOdds);
				if (randomNumber < 1) {
					IncreasePopulation (tile, new PersonCreator (Settings).CreateBabies (1));
					tile.TotalBirths++;
					Context.Log.WriteLine ("A baby was born.");
				}
			}
		}

		public void UpdatePopulationMigration(Tile tile)
		{
			// Arriving
			var probability = new Random ().Next (100);
			if (probability < 2)
			{
				var value = new Random ().Next (3);
				if (value > 0)
					Immigrate (tile, value);
			}

			// Leaving
			var leavingProbability = new Random ().Next (500);
			if (leavingProbability < tile.TotalHomelessPeople) {
				var value = new Random ().Next (1, 3);
				Emigrate (tile, value);
			}
		}

		public void IncreasePopulation(Tile tile, Person[] newPeople)
		{
			var people = new List<Person> (tile.People);
			people.AddRange (newPeople);

			tile.People = people.ToArray ();

			var data = new DataManager ();

			for (int i = 0; i < newPeople.Length; i++)
				data.Save (newPeople [i]);
		}

		public void ReducePopulation(Tile tile, int numberOfPeople)
		{
			if (tile.Population > 0) {
				if (tile.Population < numberOfPeople)
					numberOfPeople = tile.Population;
				
				var list = new List<Person> (tile.People);
				for (int i = 0; i < numberOfPeople; i++) {
					if (list.Count > 0) {
						list.RemoveAt (0);
						var person = tile.People [i];
						if (person != null)
							Context.Data.Delete (person);
					}
				}
				tile.People = list.ToArray ();
			}
		}

		public void Die(Tile tile, int numberOfPeople)
		{
			ReducePopulation (tile, numberOfPeople);
			tile.TotalDeaths += numberOfPeople;
		}

		public void Die(Tile tile, Person person)
		{
			//ReducePopulation (tile, numberOfPeople);
			tile.TotalDeaths += 1;

			var data = new DataManager ();
			data.Delete (person);

			var list = new List<Person> (tile.People);
			list.Remove (person);
			tile.People = list.ToArray ();
		}

		public void Immigrate(Tile tile, int numberOfPeople)
		{
			// TODO: Change this so instead of creating people it moves people from other tiles
			IncreasePopulation (tile, new PersonCreator().CreateAdults(numberOfPeople));

			tile.TotalImmigrants += numberOfPeople;

			var peopleWord = "people";
			if (numberOfPeople == 1)
				peopleWord = "person";
			
			Context.Log.WriteLine (numberOfPeople + " new " + peopleWord + " arrived in tile.");
		}

		public void Emigrate(Tile tile, int numberOfPeople)
		{
			ReducePopulation (tile, numberOfPeople);

			tile.TotalEmigrants += numberOfPeople;

			var peopleWord = "people";
			if (numberOfPeople == 1)
				peopleWord = "person";

			Context.Log.WriteLine (numberOfPeople + " " + peopleWord + " left tile.");
		}
	}
}

