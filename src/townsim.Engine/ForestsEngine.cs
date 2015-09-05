using System;
using townsim.Data;
using townsim.Entities;

namespace townsim.Engine
{
	public class ForestsEngine
	{
		public ForestsEngine ()
		{
		}

		public void Update(Town town)
		{
			//if (town.Forest < 1000)
			//	town.Forest = 1000;
			UpdateGrowth (town);
		}

		public void UpdateGrowth(Town town)
		{
			//var growth = town.Forest / 100000;
			//town.Forest = town.Forest + growth;
		}
	}
}

