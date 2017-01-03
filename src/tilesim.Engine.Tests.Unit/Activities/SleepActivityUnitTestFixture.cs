using System;
using NUnit.Framework;
using tilesim.Engine.Activities;
using tilesim.Engine.Entities;

namespace tilesim.Engine.Tests.Unit.Activities
{
    [TestFixture(Category="Unit")]
    public class SleepActivityUnitTestFixture : BaseEngineUnitTestFixture
    {
        [Test]
        public void Test_Sleep_ShelterAvailable()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

            var context = MockEngineContext.New ();

            context.World.Logic.AddActivity (typeof(DrinkWaterActivity));

            var settings = context.Settings;

            var person = new Person (settings);
            person.Vitals[PersonVitalType.Energy] = 0;

            person.Home = new Building (BuildingType.Shelter, settings);
            person.Home.SetPercentComplete(100);

            var needEntry = new NeedEntry (ActivityVerb.Sleep, ItemType.NotSet, PersonVitalType.Energy, 100, settings.DefaultVitalPriorities[PersonVitalType.Energy]);

            var activity = new SleepActivity (person, needEntry, settings, new ConsoleHelper(settings));

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

            activity.Act (person);

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

            Assert.AreEqual(settings.EnergyFromSleepRate, person.Vitals[PersonVitalType.Energy]);

        }
    }
}

