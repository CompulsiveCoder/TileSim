using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine.Activities
{
    public class FellWoodActivityCalculator
    {
        public FellWoodActivity Activity;

        public FellWoodActivityCalculator (FellWoodActivity activity)
        {
            Activity = activity;
        }

        public decimal GetTreeTrunkDiameter(decimal treeSize)
        {
            var sizeToDiameterRatio = 0.01m;

            var mmConversionRatio = 1000; // Millimetres in a meter

            var trunkDiameterInMM = treeSize * sizeToDiameterRatio;

            trunkDiameterInMM = trunkDiameterInMM * mmConversionRatio;
               
            return trunkDiameterInMM;
        }

        public decimal GetTreePercentageIncreaseThisCycle(Person person, Plant tree)
        {
            var diameterInMM = GetTreeTrunkDiameter (tree.Height);

            var percentageOfTreeCutThisCycle = diameterInMM / 100 * Activity.Settings.TimberFellingRate;

            return percentageOfTreeCutThisCycle;
        }

        public decimal GetActivityPercentageIncreaseThisCycle(Person person, Plant tree)
        {
            // TODO: Implement
            return 1;
            //throw new NotImplementedException ();
            /*var diameter = GetTreeTrunkDiameter (tree.Size);

            var percentageOfTreeCutThisCycle = */

            /* // TODO: Move to settings

            var percentageCutThisCycle = Settings.TimberFellingRate;

            var totalCyclesThisTree = trunkDiameter / percentageCutThisCycle;*/
        }
    }
}

