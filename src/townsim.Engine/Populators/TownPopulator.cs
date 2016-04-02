using System;
using townsim.Engine.Entities;
using System.Collections;
using System.Collections.Generic;

namespace townsim.Engine
{
	public class TownPopulator : BasePopulator
	{
		public Town Town
		{
			get { return (Town)Target; }
			set { Target = value; }
		}
		
		public TownPopulator (EngineContext context) : base(context)
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

			var people = personCreator.CreateAdults (Context.Settings.DefaultTownPopulation);

			foreach (var person in people)
				person.Town = Town;

			Town.AddLinks("People", people);*/
		}

		public void PopulateTrees()
		{
			throw new NotImplementedException ();
			/*
			var numberOfTrees = Context.Settings.DefaultTownTreeCount;

			var list = new List<Plant> ();
			for (int i = 0; i < numberOfTrees; i++) {
				var tree = new Plant(PlantType.Tree, 100, 100);
				tree.WasPlanted = false;
				tree.PercentPlanted = 100; // TODO: Is this necessary?
				list.Add (tree);
			}
			Town.Plants = list.ToArray ();*/
		}
	}
}

