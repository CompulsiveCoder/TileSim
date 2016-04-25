using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections;

namespace tilesim.Engine.Entities
{
	[Serializable]
	[JsonObject("People")]
	public class PersonCollection : List<Person>, IEnumerable
	{
		public PersonCollection ()
		{
		}

		public PersonCollection(Person[] people)
		{
			AddRange(people);
		}

	}
}
