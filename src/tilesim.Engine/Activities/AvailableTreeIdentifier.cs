﻿using System;
using System.Collections.Generic;
using tilesim.Engine.Entities;

namespace tilesim.Engine.Activities
{
    public class AvailableTreeIdentifier
    {
        public FellWoodActivity Activity { get; set; }

        public ConsoleHelper Console { get; set; }

        public AvailableTreeIdentifier (FellWoodActivity activity)
        {
            Activity = activity;
            Console = activity.Console;
        }

        public Plant[] IdentifyTreesToFell()
        {
            Console.WriteDebugLine ("  Identifying trees to fell");

            var treesToBeCutDown = new List<Plant> ();

            SelectMoreAvailableTreesIfNeeded (treesToBeCutDown);

            Console.WriteDebugLine ("   Found: " + treesToBeCutDown.Count);

            return treesToBeCutDown.ToArray();
        }

        public void SelectMoreAvailableTreesIfNeeded(List<Plant> selectedTrees)
        {
            // TODO: Can this function be improved?

            Console.WriteDebugLine ("   Selecting more trees (if needed)");


            var isEnoughTrees = IsEnoughTreesSelected (selectedTrees);

            Console.WriteDebugLine ("    Total trees selected: " + selectedTrees.Count + "");
            Console.WriteDebugLine ("    Total wood: " + CalculateTotalWood(selectedTrees) + "");

            if (!isEnoughTrees) {
                Console.WriteDebugLine ("   More trees need to be selected.");

                var tree = FindAvailableTree (selectedTrees);

                if (tree != null) {
                    selectedTrees.Add (tree);

                    SelectMoreAvailableTreesIfNeeded (selectedTrees);
                }
            } else {
                Console.WriteDebugLine ("    Enough trees have been selected.");
            }
        }

        public Plant FindAvailableTree(List<Plant> selectedTrees)
        {
            Activity.Status = "Selecting tree";

            // TODO: Use linq
            foreach (var plant in Activity.Actor.Tile.Plants) {
                if (plant.Type == PlantType.Tree
                    && plant.Height >= Activity.Settings.MinimumTreeSize
                    && !selectedTrees.Contains(plant)) {

                    Console.WriteDebugLine ("    Found large tree");

                    return plant;
                }
            }

            Console.WriteDebugLine ("  No large trees available");

            return null;
        }

        public bool IsEnoughTreesSelected(List<Plant> trees)
        {
            var total = CalculateTotalWood (trees);

            var additionalWoodNeeded = Activity.AmountOfWoodToFell - total;

            if (additionalWoodNeeded < 0)
                additionalWoodNeeded = 0;

            Console.WriteDebugLine ("    Additional wood needed: " + additionalWoodNeeded );

            return total >= Activity.AmountOfWoodToFell;
        }

        public decimal CalculateTotalWood(List<Plant> trees)
        {
            var total = 0m;

            foreach (var tree in trees)
                total += tree.TotalWood;

            return total;
        }
    }
}

