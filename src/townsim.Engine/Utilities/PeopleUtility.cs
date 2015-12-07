using System;
using System.Collections.Generic;
using townsim.Entities;

namespace townsim.Engine
{
	public class PeopleUtility
	{
		public PeopleUtility ()
		{
		}

		public Person[] NewAdults(int number)
		{
			var personCreator = new PersonCreator ();
			var list = new List<Person> ();

			for (int i = 0; i < number; i++) {
				var person = personCreator.CreateAdult ();
				list.Add (person);
			}

			return list.ToArray ();
		}

		public Person[] NewBabies(int number)
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

