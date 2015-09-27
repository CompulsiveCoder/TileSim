using System;
using NUnit.Framework;
using townsim.Entities;

namespace townsim.Engine.Tests.Unit
{
	[TestFixture]
	public class HealthEngineTestFixture
	{
		[Test]
		public void Test_Update_Thirst()
		{
			var person = new Person ();
			person.Thirst = 100;

			var healthEngine = new HealthEngine ();
			healthEngine.Update (person);

			Assert.Less (person.Health, 100);
		}

		[Test]
		public void Test_Update_Hunger()
		{
			var person = new Person ();
			person.Hunger = 100;

			var healthEngine = new HealthEngine ();
			healthEngine.Update (person);

			Assert.Less (person.Health, 100);
		}

		[Test]
		public void Test_Update_Healing()
		{
			var person = new Person ();
			person.Health = 50;

			var healthEngine = new HealthEngine ();
			healthEngine.Update (person);

			Assert.Greater (person.Health, 50);
		}
	}
}

