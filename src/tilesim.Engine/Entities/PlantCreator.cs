using System;
using tilesim.Engine.Entities;
using System.Collections.Generic;

namespace tilesim.Engine.Entities
{
	public class PlantCreator
	{
		public Random RandomGenerator = new Random();

		public EngineSettings Settings { get;set; }

		public PlantCreator (EngineSettings settings)
		{
			Settings = settings;
		}

		public Plant[] CreateTrees(int numberOfTrees)
		{
			var list = new List<Plant> ();
			for (int i = 0; i < numberOfTrees; i++) {
				var tree = CreateTree ();
				list.Add (tree);
			}
			return list.ToArray ();
		}

		public Plant CreateTree()
		{
			var tree = new Plant (PlantType.Tree);

			tree.Size = RandomGenerator.Next ((int)Settings.MinimumTreeSize, 100);

			return tree;

		}
	}
}

