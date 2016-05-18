using System;
using NUnit.Framework;
using tilesim.Engine.Entities;

namespace tilesim.Engine.Tests.Integration
{
    [TestFixtureAttribute(Category="Integration")]
    public class GetTiredAndSleepIntegrationTestFixture : BaseEngineIntegrationTestFixture
    {
        [Test]
        public void Test_GetTiredAndSleep()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

            var context = MockEngineContext.New ();

            context.Settings.DefaultGatherFoodRate = 50; // Increase the rate of food gathering so the test goes faster
            context.Settings.DefaultEatAmount = 100; // Increase the amount the person eats so the test goes faster

            context.World.Logic.AddNeed (new SleepNeedIdentifier (context.Settings, context.Console));
            context.World.Logic.AddActivity (typeof(SleepActivity));

            var tile = context.World.Tiles [0];

            var person = context.World.PersonCreator.CreateAdult();

            person.Vitals[PersonVitalType.Energy] = 0;

            tile.AddPerson (person);

            context.Player = person;

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

            context.Initialize (); // TODO: Should Start be part of the test? Or part of the preparation before the above console output?

            var numberOfCycles = 5;

            context.Run (numberOfCycles);

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

            var expectedEnergy = context.Settings.EnergyFromSleepRate * numberOfCycles / 2; // Divide by 2 because the person has no shelter.

            Assert.AreEqual (expectedEnergy, person.Vitals[PersonVitalType.Energy]);
        }
    }
}

