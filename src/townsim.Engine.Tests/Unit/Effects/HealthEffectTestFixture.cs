using System;
using NUnit.Framework;
using townsim.Entities;
using townsim.Engine.Effects;

namespace townsim.Engine.Tests.Unit.Effects
{
	[TestFixture]
	public class HealthEffectTestFixture
	{
		[Test]
		public void Test_Update_Thirst()
		{
			var person = new Person ();
			person.Thirst = 100;

			var healthEffect = new HealthEffect ();
			healthEffect.Update (person);

			Assert.Less (person.Health, 100);
		}

		[Test]
		public void Test_Update_Hunger()
		{
			var person = new Person ();
			person.Hunger = 100;

			var healthEffect = new HealthEffect ();
			healthEffect.Update (person);

			Assert.Less (person.Health, 100);
		}

		[Test]
		public void Test_Update_Healing()
		{
			var person = new Person ();
			person.Health = 50;

			var healthEffect = new HealthEffect ();
			healthEffect.Update (person);

			Assert.Greater (person.Health, 50);
		}
	}
}

