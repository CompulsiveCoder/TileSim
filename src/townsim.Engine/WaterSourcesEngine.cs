using System;
using townsim.Data;
using townsim.Entities;
using townsim.Alerts;

namespace townsim.Engine
{
	public class WaterSourcesEngine
	{

		public WaterSourcesEngine ()
		{
		}

		public void Update(Town town)
		{
			Rain (town);

			//ConsumeWater (town);

			//DieOfThirst (town);
		}

		/*public void ConsumeWater(Town town)
		{
			foreach (var person in town.People) {
				ConsumeWater (person);
			}

			//var amount = town.Population * WaterConsumptionRate;

			var populationEngine = new PopulationEngine ();

			if (amount > town.WaterSources)
				populationEngine.Die(town, 2);
			
			town.WaterSources -= amount;

			if (town.WaterSources < 0)
				town.WaterSources = 0;
		}*/

		/*public void ConsumeWater(Town town, Person person)
		{
			bool isThirsty = (new Random ().Next (10) < 2);

			if (isThirsty)
				town.WaterSources -= WaterConsumptionRate;
		}*/

		public void Rain(Town town)
		{
			var probability = new Random ().Next (100);
			if (probability > 98)
			{
				var value = new Random ().Next (1000);
				town.WaterSources += value;
			}
		}

		/*public void DieOfThirst(Town town)
		{
			var peopleEngine = new PopulationEngine ();
			if (town.Population > town.WaterSources) {
				var shortage = town.Population - town.WaterSources;
				//var numberOfPeople = (int)shortage;// (int)(shortage / 10);
				var numberOfPeople = new Random().Next(0, (int)shortage);
				if (numberOfPeople > 0) {
					town.AddAlert (new DehydrationAlert ());
					peopleEngine.Die (town, numberOfPeople);
				} else
					town.AddAlert (new ThirstAlert ());
			}
		}*/
	}
}

