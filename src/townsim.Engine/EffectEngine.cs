using System;
using townsim.Engine.Effects;
using townsim.Entities;

namespace townsim.Engine
{
	public class EffectEngine
	{
		public EngineContext Context { get;set; }

		public EffectEngine (EngineContext context)
		{
			Context = context;
		}

		public void ApplySingleCycle()
		{
			var thirstEffect = new ThirstEffect (Context);
			var hungerEffect = new HungerEffect (Context);
			var populationEffect = new PopulationEffect (Context);
			var treeGrowthEffect = new TreeGrowthEffect (Context);
			var rainEffect = new RainEffect (Context);
			var plantGrowthEffect = new PlantGrowthEffect (Context);

			var healthEffect = new HealthEffect (Context);

			// Local people
			foreach (var plant in Context.Data.Get<Plant>()) {
				plantGrowthEffect.Update (plant);
			}

			foreach (var person in Context.Data.Get<Person>()) {
				hungerEffect.Update (person);
				thirstEffect.Update (person);
				healthEffect.Update (person);
			}


			foreach (var town in Context.Data.Get<Town>()) {
				// Local environment
				rainEffect.Update (town);
				treeGrowthEffect.Update (town);

				// Global, population and migration
				populationEffect.Update(town);
			}

		}
	}
}

