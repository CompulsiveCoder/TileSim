using System;
using System.Collections.Generic;

namespace townsim.Entities
{
	public class PersonCreator
	{
		public PersonCreator ()
		{
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
			var person = new Person ();
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
			var person = new Person ();
			person.Gender = GetRandomGender ();
			person.Age = GetRandomAge (18, 50);
			return person;
		}

		public Gender GetRandomGender()
		{
			var value = new Random (DateTime.Now.Millisecond).Next (0, 10);
			return (value <= 5 ? Gender.Male : Gender.Female);
		}

		public double GetRandomAge(int minimumAge, int maximumAge)
		{
			var age = new Random (DateTime.Now.Millisecond).Next (minimumAge, maximumAge);
			return age;
		}
	}
}

