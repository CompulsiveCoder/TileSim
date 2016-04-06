using System;
using System.Collections.Generic;

namespace townsim.Engine.Entities
{
    [Serializable]
	public class PersonCreator
	{
		Random Randomiser = new Random();

        public EngineSettings Settings { get;set; }

        public PersonCreator (EngineSettings settings)
		{
            Settings = settings;
		}

		public Person[] CreateBabies(int numberOfBabies)
		{
			var list = new List<Person> ();
			for (int i = 0; i < numberOfBabies; i++)
				list.Add (CreateBaby ());
			return list.ToArray ();
		}

		public Person CreateBaby()
		{
            var person = new Person (Settings);
			person.Gender = GetRandomGender ();
			return person;
		}

		public Person[] CreateAdults(int numberOfAdults)
		{
			var list = new List<Person> ();
			for (int i = 0; i < numberOfAdults; i++)
				list.Add (CreateAdult ());
			return list.ToArray ();
		}

		public Person CreateAdult()
		{
			var person = new Person (Settings);
			person.Gender = GetRandomGender ();
			person.Age = GetRandomAge (18, 50);
			return person;
		}

		public Gender GetRandomGender()
		{
			var value = Randomiser.Next (0, 10);
			return (value <= 5 ? Gender.Male : Gender.Female);
		}

		public double GetRandomAge(int minimumAge, int maximumAge)
		{
			var age = Randomiser.Next (minimumAge, maximumAge);
			return age;
		}
	}
}

