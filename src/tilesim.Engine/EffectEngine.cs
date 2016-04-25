using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine
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

            throw new NotImplementedException ();

			/*var thirstEffect = new ThirstEffect (Context);
			var hungerEffect = new HungerEffect (Context);
			//var populationEffect = new PopulationEffect (Context);
			var treeGrowthEffect = new TreeGrowthEffect (Context);
			var rainEffect = new RainEffect (Context);
			var plantGrowthEffect = new PlantGrowthEffect (Context);

			var healthEffect = new HealthEffect (Context);*/

			// Local people
			/*foreach (var plant in Context.Data.Get<Plant>()) {
				plantGrowthEffect.Update (plant);
			}

			foreach (var person in Context.Data.Get<Person>()) {
				hungerEffect.Update (person);
				thirstEffect.Update (person);
				healthEffect.Update (person);
			}


			foreach (var tile in Context.Data.Get<Tile>()) {
				// Local environment
				rainEffect.Update (tile);
				treeGrowthEffect.Update (tile);

				// Global, population and migration
			//	populationEffect.Update(tile);
			}*/

		}
	}
}

