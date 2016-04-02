using System;
using System.Collections.Generic;
using townsim.Entities;

namespace townsim.Engine
{
	public class PeopleCreator
	{
		public PeopleCreator ()
		{
		}

		public Person CreateAdult()
		{
			var people = CreateAdults (1);

			return people [0];
		}

		public Person[] CreateAdults(int number)
		{
			var personCreator = new PersonCreator ();
			var list = new List<Person> ();

			for (int i = 0; i < number; i++) {
				var person = personCreator.CreateAdult ();
				list.Add (person);
			}

			return list.ToArray ();
		}

		public Person[] CreateBabies(int number)
		{
			var personCreator = new PersonCreator ();
			var list = new List<Person> ();

			for (int i = 0; i < number; i++) {
				var person = personCreator.CreateBaby ();
				list.Add (person);
			}

			return list.ToArray ();
		}
	}
}

