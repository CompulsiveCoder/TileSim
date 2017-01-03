using System;
using tilesim.Engine.Entities;
using System.Collections.Generic;
using tilesim.Engine.Needs;

namespace tilesim.Engine.Activities
{
    [Activity(ActivityVerb.Fell, ItemType.Wood, PersonVitalType.NotSet)]
	[Serializable]
	public class FellWoodActivity : BaseActivity
	{
        #region Properties
		public decimal TotalWoodFelled = 0;

        public Plant[] TreesToFell;

		public decimal AmountOfWoodToFell
		{
			get { return NeedEntry.Quantity; }
			set { NeedEntry.Quantity = value; }
		}

        public FellWoodActivityCalculator Calculator;

        public PercentageTracker Percentage;

        public AvailableTreeIdentifier TreeIdentifier;
        #endregion

        #region Constructor
        public FellWoodActivity (Person person, NeedEntry needEntry, EngineSettings settings, ConsoleHelper console)
			: base(person, needEntry, settings, console)
        {
            Calculator = new FellWoodActivityCalculator (this);
            TreeIdentifier = new AvailableTreeIdentifier (this);
		}
        #endregion

        #region Functions
		public override void Prepare (Person person)
		{
			throw new NotImplementedException ();
		}

		public override bool CheckFinished ()
		{
			return TotalWoodFelled >= AmountOfWoodToFell;
		}

		public override void Execute (Person person)
		{			
			var treesBeingFelled = GetTreesToFell ();

            var totalDiameter = GetTotalDiameter(treesBeingFelled);

            if (Percentage == null)
                Percentage = new PercentageTracker (totalDiameter);

			if (treesBeingFelled != null
                && treesBeingFelled.Length > 0) {
                var treeBeingFelled = treesBeingFelled [0];

				var isFinishedFellingTree = FellTreeCycle (Actor, treeBeingFelled);

				if (isFinishedFellingTree)
					FinishedFellingSingleTree (Actor, treeBeingFelled);
			} else
				throw new Exception ("Can't fell wood when no trees are available.");
		}

        public decimal GetTotalDiameter(Plant[] treesBeingFelled)
        {
            var total = 0m;

            foreach (var tree in treesBeingFelled)
                total += Calculator.GetTreeTrunkDiameter(tree.Height);

            return total;
        }

        public override bool IsActorAbleToAct (Person actor)
        {
            // TODO: Implement the need for an axe
            return true;
        }

        public override void RegisterNeeds (Person actor)
        {
            // TODO: Implement the need for an axe
        }

		public bool IsTimberAvailable(GameTile tile)
		{
            throw new NotImplementedException ();
			//return tile.Timber > 0;
		}

		public bool FellTreeCycle(Person person, Plant tree)
		{
            var previousPercentageHarvested = tree.PercentHarvested;

            Status = "Chopping down tree " + (int)tree.PercentHarvested + "%";

			if (Settings.IsVerbose)
				Console.WriteDebugLine ("  Felling tree");

            // 1) Calculate distance needed to cut through tree
            var diameterInMM = Calculator.GetTreeTrunkDiameter (tree.Height);

            Console.WriteDebugLine ("    Tree:");
            //Console.WriteDebugLine ("    Percentage increase:" + treePercentageIncrease);
            Console.WriteDebugLine ("      Height (m): " + tree.Height);
            Console.WriteDebugLine ("      Diameter (mm): " + diameterInMM);
            Console.WriteDebugLine ("      Wood: " + tree.TotalWood);


            Console.WriteDebugLine ("    Summary:");

            // 2) Calculate distance cut this cycle
            var distanceCutInMM = Settings.TimberFellingRate;

            Console.WriteDebugLine ("      Distance cut this cycle (mm): " + distanceCutInMM);

            // 3) Calculate percentage point unit for distance cut through tree

            //IncreasePercentageHarvested (plant);

            var percentageUnit = diameterInMM / 100;

            // 4) Calculate tree percentage cut this cycle

            var percentageCutThisCycle = percentageUnit * distanceCutInMM;

            // 5) Increase tree percentage harvested
            tree.PercentHarvested += percentageCutThisCycle;

            if (tree.PercentHarvested > 100)
                tree.PercentHarvested = 100;

            // TODO Clean up
            Console.WriteDebugLine ("      Percentage felled (before): " + previousPercentageHarvested);
            Console.WriteDebugLine ("      Percentage felled (this cycle): " + percentageUnit);
            Console.WriteDebugLine ("      Percentage felled (after): " + tree.PercentHarvested);

            //var percentageCut = percentageUnit *;

            //Console.WriteDebugLine ("    Percentage cut (tree): " + percentageUnit + "%);

            // 6) Calculate total distance needed to cut through all trees
            decimal totalDistanceInMM = 0;

            foreach (var t in TreesToFell)
            {
                var d = Calculator.GetTreeTrunkDiameter (t.Height);

                totalDistanceInMM += d;
            }

            Console.WriteDebugLine ("      Total distance to cut (mm): " + totalDistanceInMM);

            // 7) Calculate total percentage point unit

            var totalPercentageUnit = totalDistanceInMM / 100;

            // 8) Calculate total percentage cut this cycle

            // 9) Calculate total percentage cut
            // TODO: Fix this equation
            var distanceFraction = totalDistanceInMM/distanceCutInMM;
            var percentageIncrease = totalPercentageUnit * distanceFraction;
            IncreasePercentComplete(percentageIncrease);

            Console.WriteDebugLine ("      Total percentage unit: " + totalPercentageUnit);
            Console.WriteDebugLine ("      Activity percent complete: " + PercentComplete);

            return tree.PercentHarvested == 100;


		}
		public void FinishedFellingSingleTree(Person person, Plant tree)
		{
            Status = "Finished felling tree";
			person.Tile.RemovePlant (tree);

            var amountOfWood = tree.Height * Settings.TreeHeightToWoodAmountRatio;

			ItemsProduced [ItemType.Wood] = amountOfWood;

            if (Settings.IsVerbose) {
				Console.WriteDebugLine ("    Wood from tree: " + amountOfWood);
                Console.WriteDebugLine ("    Total wood: " + person.Inventory.Items [ItemType.Wood] + amountOfWood);
			}

            RemoveFromTreesToFellList (tree);

            if (TreesToFell.Length == 0) {
                Finish ();
            }
			
            // TODO: Clean up
			//if (Context.Settings.PlayerId == person.Id)
				//Context.Log.WriteLine (String.Format("Player cut a tree down. Age:{0} size:{1} wood:{2}", (int)tree.Age, (int)tree.Size, (int)amountOfWood));
		}

        public void UpdateStatusForCycle(Person person, Plant tree)
        {
            throw new NotImplementedException ();

        }

        public void RemoveFromTreesToFellList(Plant tree)
        {
            var treesToFell = new List<Plant> (TreesToFell);
            treesToFell.Remove (tree);
            TreesToFell = treesToFell.ToArray ();
        }

		public Plant[] GetTreesToFell()
		{
            if (TreesToFell == null)
              TreesToFell = TreeIdentifier.IdentifyTreesToFell();

            return TreesToFell;
		}
        #endregion
	}
}

