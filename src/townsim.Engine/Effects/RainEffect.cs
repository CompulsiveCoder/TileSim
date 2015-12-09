﻿using System;
using townsim.Data;
using townsim.Entities;
using townsim.Alerts;

namespace townsim.Engine.Effects
{
	public class RainEffect
	{
		public EngineSettings Settings { get;set; }

		public decimal RainRate = 0.05m;

		public RainEffect (EngineSettings settings)
		{
			Settings = settings;
		}

		public void Update(Town town)
		{
			Rain (town);
		}

		public void Rain(Town town)
		{
			var probability = new Random ().Next (100);
			if (probability > 98)
			{
				var randomValue = new Random ().Next (10);
				var actualValue = (decimal)randomValue * RainRate;
				town.WaterSources += actualValue;

        LogWriter.Current.AppendLine (CurrentEngine.Id, "It rained " + actualValue + "litres");
			}
		}
	}
}

