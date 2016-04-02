using System;
using NUnit.Framework;
using townsim.Entities;
using townsim.Engine.Effects;

namespace townsim.Engine.Tests.Unit.Effects
{
	// TODO: Overhaul and re-enable
	//[TestFixture]
	public class HealthEffectTestFixture
	{
		// TODO: Overhaul and re-enable
		//[Test]
		public void Test_Update_Thirst()
		{
			throw new NotImplementedException ();
			/*var context = EngineContext.New();
			context.Settings.DefaultTownPopulation = 1;
			context.Populate ();

			var person = context.World.People [0];

			person.Thirst = 100;

			var healthEffect = new HealthEffect (context);
			healthEffect.Update (person);

			Assert.Less (person.Health, 100);*/
		}

		// TODO: Overhaul and re-enable
		//[Test]
		public void Test_Update_Hunger()
		{
			throw new NotImplementedException ();
			/*var context = EngineContext.New();
			context.Settings.DefaultTownPopulation = 1;
			context.Populate ();

			var person = context.World.People [0];

			person.Hunger = 100;

			var healthEffect = new HealthEffect (context);
			healthEffect.Update (person);

			Assert.Less (person.Health, 100);*/
		}

		// TODO: Overhaul and re-enable
		//[Test]
		public void Test_Update_Healing()
		{
			throw new NotImplementedException ();
			/*var context = EngineContext.New();
			context.Settings.DefaultTownPopulation = 1;
			context.Populate ();

			var person = context.World.People [0];

			person.Health = 50;

			var healthEffect = new HealthEffect (context);
			healthEffect.Update (person);

			Assert.Greater (person.Health, 50);*/
		}
	}
}

