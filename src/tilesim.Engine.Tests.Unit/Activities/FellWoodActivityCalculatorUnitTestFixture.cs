using System;
using NUnit.Framework;
using tilesim.Engine.Activities;
using tilesim.Engine.Entities;

namespace tilesim.Engine.Tests.Unit.Activities
{
    [TestFixture]
    public class FellWoodActivityCalculatorUnitTestFixture : BaseEngineUnitTestFixture
    {
        [Test]
        public void Test_GetTreePercentageIncreaseThisCycle()
        {
            var context = MockEngineContext.New ();
            context.PopulateFromSettings ();

            var needEntry = new NeedEntry (ActivityVerb.Fell, ItemType.Wood, PersonVitalType.NotSet, 100, 100);

            var activity = new FellWoodActivity (context.Player, needEntry, context.Settings, context.Console);

            var calculator = new FellWoodActivityCalculator (activity);

            var tree = context.Player.Tile.Trees [0];

            tree.Height = 10;

            var percentageIncrease = calculator.GetTreePercentageIncreaseThisCycle(context.Player, tree);

            Assert.AreEqual (10, percentageIncrease);
        }

        [Test]
        public void Test_GetTreeTrunkDiameter()
        {
            var context = MockEngineContext.New ();
            context.PopulateFromSettings ();

            var needEntry = new NeedEntry (ActivityVerb.Fell, ItemType.Wood, PersonVitalType.NotSet, 100, 100);

            var activity = new FellWoodActivity (context.Player, needEntry, context.Settings, context.Console);

            var calculator = new FellWoodActivityCalculator (activity);

            var diameter = calculator.GetTreeTrunkDiameter (10);

            Assert.AreEqual (100, diameter);
        }
    }
}

