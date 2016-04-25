using System;
using tilesim.Engine.Entities;
using System.Collections;
using System.Collections.Generic;

namespace tilesim.Engine
{
	public class TilePopulator : BasePopulator
	{
		public GameTile Tile
		{
			get { return (GameTile)Target; }
			set { Target = value; }
		}
		
		public TilePopulator (EngineContext context) : base(context)
		{
		}

		public override void Populate()
		{
			PopulatePeople ();

			PopulateTrees ();
		}

		public void PopulatePeople()
		{
			throw new NotImplementedException ();
			// TODO: move to property
			/*var personCreator = new PersonCreator ();

			var people = personCreator.CreateAdults (Context.Settings.DefaultTilePopulation);

			foreach (var person in people)
				person.Tile = Tile;

			Tile.AddLinks("People", people);*/
		}

		public void PopulateTrees()
		{
			throw new NotImplementedException ();
			/*
			var numberOfTrees = Context.Settings.DefaultTileTreeCount;

			var list = new List<Plant> ();
			for (int i = 0; i < numberOfTrees; i++) {
				var tree = new Plant(PlantType.Tree, 100, 100);
				tree.WasPlanted = false;
				tree.PercentPlanted = 100; // TODO: Is this necessary?
				list.Add (tree);
			}
			Tile.Plants = list.ToArray ();*/
		}
	}
}

