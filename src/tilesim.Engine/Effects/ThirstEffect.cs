using System;
using tilesim.Engine.Entities;
using tilesim.Engine.Activities;

namespace tilesim.Engine.Effects
{
	public class ThirstEffect : BasePersonEffect
	{
		public EngineInfo Info { get;set; }

        public ThirstEffect (EngineSettings settings, ConsoleHelper console) : base(settings, console)
		{
		}

        public override bool IsApplicable (Person person)
        {
            var personIsDrinking = (person.ActivityText == typeof(DrinkWaterActivity).Name);
            return person.IsAlive && !personIsDrinking;
        }

        public override void Execute (Person person)
        {
            VitalsChange.Add (PersonVitalType.Thirst, +Settings.ThirstRate);
		}
	}
}

