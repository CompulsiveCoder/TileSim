using System;
using datamanager.Entities;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Xml.Serialization;
using tilesim.Engine.Entities;

namespace tilesim.Engine
{
	[Serializable]
    public class GameEnvironment : BaseGameEntity
	{
        [XmlIgnore]
        public GameEnvironmentPopulator Populator { get;set; }

		public EngineContext Context { get; set; }

		[OneWay]
		public Person[] People { get;set; }

		[OneWay]
		public Plant[] Plants { get; set; }

		[OneWay]
		public GameTile[] Tiles { get; set; }

		[JsonIgnore]
		[XmlIgnore]
		[NonSerialized]
		public PersonCreator PersonCreator;

		[JsonIgnore]
		[XmlIgnore]
		[NonSerialized]
		public PlantCreator PlantCreator;

		public GameLogic Logic { get; set; }

		public GameEnvironment (EngineContext context)
        {
            Context = context;

            Populator = new GameEnvironmentPopulator (this);
            PersonCreator = new PersonCreator (context.Settings);
			PlantCreator = new PlantCreator (context.Settings);
			Tiles = new GameTile[]{};
			People = new Person[] {};
			Plants = new Plant[]{ };
			Logic = new GameLogic ();
			Tiles = new GameTile[]{new GameTile(this)};
		}

		public void AddPerson(Person person)
		{
			AddPeople (person);
		}

		public void AddPeople(params Person[] people)
		{
			var list = new List<Person> (People);
			list.AddRange (people);
			People = list.ToArray ();
		}

		public void AddTrees(Plant[] trees)
		{
			var list = new List<Plant> (Plants);
			list.AddRange (trees);
			Plants = list.ToArray ();
		}
	}
}

