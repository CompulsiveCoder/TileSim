using System;
using townsim.Entities;
using Sider;

namespace townsim.Data
{
	public class PeopleReader : BaseDataAdapter
	{
		public PeopleReader ()
		{
		}

		public Person[] Read(Guid townId)
		{
			var client = new RedisClient ();
			var key = new PeopleKeys ().GetPeopleKey (townId);

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

