using System;
using Sider;
using System.IO;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using tilesim.Entities;

namespace tilesim.Data
{
	public class PersonSaver : BaseDataAdapter
	{

		public PersonSaver ()
		{

		}

		public void Save(Person person)
		{
			var client = new RedisClient();
			var key = new PeopleKeys ().GetPersonKey (person.Id);
			var json = person.ToJson ();
			client.Set(key, json);

			var idManager = new DataIdManager ();
			idManager.Add (person);

		}

		public void Save(Tile tile, Person[] people)
		{
			foreach (var person in people)
				Save (person);
			
			var client = new RedisClient();
			var key = new PeopleKeys ().GetPeopleKey (tile.Id);
			var json = ArrayToJson (people);
			client.Set(key, json);
		}
	}
}

