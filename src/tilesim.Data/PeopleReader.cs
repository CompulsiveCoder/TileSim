using System;
using tilesim.Entities;
using Sider;

namespace tilesim.Data
{
	public class PeopleReader : BaseDataAdapter
	{
		public PeopleReader ()
		{
		}

		public Person[] Read(Guid tileId)
		{
			var client = new RedisClient ();
			var key = new PeopleKeys ().GetPeopleKey (tileId);

			if (!client.Exists (key))
				return new Person[]{ };
			else {
				var json = client.Get (key);

				var people = JsonToArray<Person> (json);

				return people;
			}
		}
	}
}

