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

		public void Add(string id)
		{
			var client = new RedisClient ();
			var key = new EngineKeys().GetEngineIdsKey();
			var ids = GetIds ();
			var list = new List<string>();

			if (ids.Length > 0)
				list.AddRange (ids);

			if (!list.Contains (id))
				list.Add (id);
			
			client.Set(key, String.Join(",", list.ToArray()));

		}

		public string[] GetIds()
		{
			var client = new RedisClient ();
			var key = new EngineKeys().GetEngineIdsKey();
			if (client.Exists (key)) {
				var data = client.Get (key);

				return data.Split (',');
			} else {
				return new string[]{ };
			}
		}

		public void Remove(string id)
		{
			var client = new RedisClient ();
			var key = new EngineKeys().GetEngineIdsKey();
			var ids = GetIds ();
			var list = new List<string>();

			if (ids.Length > 0)
				list.AddRange (ids);

			if (list.Contains (id))
				list.Remove (id);

			client.Set(key, String.Join(",", list.ToArray()));

		}
	}
}

