using System;
using tilesim.Data;
using tilesim.Engine.Entities;
using tilesim.Alerts;

namespace tilesim.Engine.Effects
{
	public class RainEffect : BaseEffect
	{
		public decimal RainRate = 0.05m;

		public Random Random = new Random ();

		public RainEffect (EngineContext context) : base(context)
		{
		}

		public void Update(Tile tile)
		{
			Rain (tile);
		}

		public void Rain(Tile tile)
		{
			var probability = Random.Next (100);
			if (probability > 98)
			{
				var randomValue = Random.Next (10);
				var actualValue = (decimal)randomValue * RainRate;
				tile.WaterSources += actualValue;

				Context.Log.WriteLine ("It rained " + actualValue + "litres");
			}
		}
	}
}

