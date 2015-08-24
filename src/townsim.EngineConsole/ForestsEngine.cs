using System;
using townsim.Data;

namespace townsim.EngineConsole
{
	public class ForestsEngine
	{
		public ForestsEngine ()
		{
		}

		public void Update(Town town)
		{
			if (town.Forest < 10)
				town.Forest = 10;
			if (town.Forest > 10000)
				town.Forest = 1000;
			UpdateGrowth (town);
		}

		public void UpdateGrowth(Town town)
		{
			var growth = town.Forest / 200;
			town.Forest = town.Forest + growth;
		}
	}
}

