using System;
using Sider;
using System.Collections.Generic;

namespace townsim.Data
{
	public class EngineIdManager
	{
		public EngineIdManager ()
		{
		}

		public void Add(Guid id)
		{
			var client = new RedisClient ();
			var key = new EngineKeys().GetEngineIdsKey();
			var ids = GetIds ();
			var list = new List<Guid>();

			if (ids.Length > 0)
				list.AddRange (ids);

			if (!list.Contains (id))
				list.Add (id);
			
			client.Set(key, String.Join(",", list.ToArray()));

		}

		public Guid[] GetIds()
		{
			var client = new RedisClient ();
			var key = new EngineKeys().GetEngineIdsKey();
			if (client.Exists (key)) {
				var data = client.Get (key);

				var idStrings = data.Split (',');

				var ids = new List<Guid> ();

				foreach (var idString in idStrings) {
					ids.Add (Guid.Parse (idString));
				}

				return ids.ToArray ();
			} else {
				return new Guid[]{ };
			}
		}

		public void Remove(Guid id)
		{
			var client = new RedisClient ();
			var key = new EngineKeys().GetEngineIdsKey();
			var ids = GetIds ();
			var list = new List<Guid>();

			if (ids.Length > 0)
				list.AddRange (ids);

			if (list.Contains (id))
				list.Remove (id);

			client.Set(key, String.Join(",", list.ToArray()));

		}
	}
}

