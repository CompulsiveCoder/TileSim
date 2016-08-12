using System;
using NUnit.Framework;
using tilesim.Engine.Activities;
using tilesim.Engine.Entities;

namespace tilesim.Engine.Tests.Integration
{
    [TestFixture(Category="Integration")]
    public class StartActivityOrderIntegrationTestFixture : BaseEngineIntegrationTestFixture
    {
        [Test]
        public void Test_AddOrder_StartActivityOrder()
        {

            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

            var context = MockEngineContext.New ();
            context.Data.IsVerbose = true;
            context.PopulateFromSettings ();
            context.AddCompleteLogic ();

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

            context.Initialize (); // TODO: Should Start be part of the test? Or part of the preparation before the above console output?

            var order = new StartActivityOrder (context.Player, ActivityVerb.Fell, ItemType.Wood, 100);

            context.AddOrder (order);

            context.Run (1);

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

            Assert.IsNotNull (context.Player.Activity);
          /*  
            Assert.AreEqual (100, person.Home.PercentComplete);
            Assert.AreEqual (null, person.Activity);   */
        }
    }
}

