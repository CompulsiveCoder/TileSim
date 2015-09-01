using System;
using Sider;
using System.IO;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using townsim.Entities;

namespace townsim.Data
{
	public class TownSaver
	{

		public TownSaver ()
		{
			
		}

		public void Save(Town town)
		{
			var client = new RedisClient();
			var key = new TownKeys ().GetTownKey (town.Id);
			var json = town.ToJson ();
			client.Set(key, json);

			var idManager = new DataIdManager ();
			idManager.Add (town);

			// Buildings
			var buildingSaver = new BuildingSaver ();
			buildingSaver.Save (town, town.Buildings.ToArray());

			// People
			var personSaver = new PersonSaver ();
			personSaver.Save (town, town.People);

			//var townPopulation = new TownPopulation ();
			//townPopulation.SetPopulationCount (town.Id, town.Population);
		}
	}
}

