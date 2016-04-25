using System;
using Sider;
using System.IO;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using tilesim.Entities;
using datamanager.Data;

namespace tilesim.Data
{
	public class TileSaver
	{

		public TileSaver ()
		{
			
		}

		public void Save(Tile tile)
		{
			new DataManager ().Save (tile);
			/*var client = new RedisClient();
			var key = new TileKeys ().GetTileKey (tile.Id);
			var json = tile.ToJson ();
			client.Set(key, json);

			var idManager = new DataIdManager ();
			idManager.Add (tile);

			// Buildings
			var buildingSaver = new BuildingSaver ();
			buildingSaver.Save (tile, tile.Buildings.ToArray());

			// People
			var personSaver = new PersonSaver ();
			personSaver.Save (tile, tile.People);
*/
			//var tilePopulation = new TilePopulation ();
			//tilePopulation.SetPopulationCount (tile.Id, tile.Population);
		}
	}
}

