using System;
using Sider;

namespace tilesim.Data
{
	public class PersonDeleter : BaseDataAdapter
	{
		public PersonDeleter ()
		{
		}

		public void Delete(Guid personId)
		{
			var client = new RedisClient ();
			var key = new PeopleKeys ().GetPersonKey (personId);

			if (client.Exists (key))
				client.Del (key);
		}
	}
}

