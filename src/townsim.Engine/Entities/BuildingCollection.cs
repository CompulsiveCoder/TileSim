using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections;

namespace townsim.Entities
{
	[Serializable]
	[JsonObject("Buildings")]
	public class BuildingCollection : List<Building>, IEnumerable
	{
		public BuildingCollection ()
		{
		}

		public BuildingCollection(Building[] buildings)
		{
			AddRange(buildings);
		}

		public int TotalCompleted
		{
			get {
				int total = 0;
				foreach (var building in this) {
					if (building.IsCompleted)
						total++;
				}
				return total;
			}
		}

		public int TotalHouses
		{
			get {
				int total = 0;
				foreach (var building in this) {
					if (building.Type == BuildingType.House)
						total++;
				}
				return total;
			}
		}

		public int TotalCompletedHouses
		{
			get {
				int total = 0;
				foreach (var building in this) {
					if (building.Type == BuildingType.House
						&& building.IsCompleted)
						total++;
				}
				return total;
			}
		}

		public int TotalIncompleteHouses
		{
			get {
				int total = 0;
				foreach (var building in this) {
					if (building.Type == BuildingType.House
						&& !building.IsCompleted)
						total++;
				}
				return total;
			}
		}

		public Building[] Houses
		{
			get {
				var houses = new List<Building> ();
				foreach (var building in this) {
					if (building.Type == BuildingType.House)
						houses.Add (building);
				}

				return houses.ToArray();
			}
		}

		public Building[] IncompleteHouses
		{
			get {
				var houses = new List<Building> ();
				foreach (var building in this) {
					if (building.Type == BuildingType.House
						&& !building.IsCompleted)
						houses.Add (building);
				}

				return houses.ToArray();
			}
		}

		public double AveragePercentComplete
		{
			get {
				if (IncompleteHouses.Length == 0)
					return 0;
				double sum = 0;
				foreach (var building in Houses) {
					sum += building.PercentComplete;

				}
				return sum / TotalHouses;
			}
		}
	}
}
