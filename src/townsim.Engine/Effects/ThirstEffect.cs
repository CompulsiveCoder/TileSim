using System;
using townsim.Engine.Entities;
using townsim.Data;
using townsim.Engine.Activities;

namespace townsim.Engine.Effects
{
	public class ThirstEffect : BasePersonEffect
	{
        // TODO: Clean up
		public decimal ThirstRate = 0.3m;//100m / (24*60*60) * 5m; // 100% / seconds in a day * drinks per day

		public EngineInfo Info { get;set; }

        public ThirstEffect (EngineSettings settings, ConsoleHelper console) : base(settings, console)
		{
		}

        public override bool IsApplicable (Person person)
        {
            var personIsDrinking = (person.ActivityName == typeof(DrinkWaterActivity).Name);
            return person.IsAlive && !personIsDrinking;
        }

        public override void Execute (Person person)
        {
            VitalsChange.Add (PersonVital.Thirst, +ThirstRate);
		}
	}
}

