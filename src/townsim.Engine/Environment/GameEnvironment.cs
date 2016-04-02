﻿using System;
using datamanager.Entities;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Xml.Serialization;
using townsim.Engine.Entities;

namespace townsim.Engine
{
	[Serializable]
    public class GameEnvironment : BaseGameEntity
	{
		public EngineContext Context { get; set; }

		[OneWay]
		public Town[] Towns { get; set; }

		[OneWay]
		public Person Player { get; set; }

		[OneWay]
		public Person[] People { get;set; }

		[OneWay]
		public Plant[] Plants { get; set; }

		[OneWay]
		public GameTile[] Tiles { get; set; }

		[JsonIgnore]
		[XmlIgnore]
		[NonSerialized]
		public PeopleCreator PeopleCreator;

		[JsonIgnore]
		[XmlIgnore]
		[NonSerialized]
		public PlantCreator PlantCreator;

		public GameLogic Logic { get; set; }

		public GameEnvironment (EngineContext context)
		{
			PeopleCreator = new PeopleCreator ();
			PlantCreator = new PlantCreator (context.Settings);
			Context = context;
			Towns = new Town[]{};
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
