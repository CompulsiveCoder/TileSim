using System;
using System.Runtime.Remoting.Messaging;
using System.Collections.Generic;
using townsim.Engine.Activities;
using townsim.Engine.Needs;
using townsim.Engine.Entities;
using townsim.Engine.Effects;

namespace townsim.Engine
{
	[Serializable]
	public class GameLogic
    {
        public ActivityInfo[] Activities { get; set; }
        public BaseEffect[] Effects { get; set; }
		public BaseNeedIdentifier[] Needs { get; set; }

		public GameLogic ()
		{
            Effects = new BaseEffect[]{ };
			Needs = new BaseNeedIdentifier[]{ };
			Activities = new ActivityInfo[]{ };
        }

        public void AddEffect(BaseEffect effect)
        {
            var list = new List<BaseEffect> ();
            if (Effects != null)
                list.AddRange (Effects);
            list.Add(effect);
            Effects = list.ToArray ();
        }


		public void AddNeed(BaseNeedIdentifier need)
		{
			var list = new List<BaseNeedIdentifier> ();
			if (Needs != null)
				list.AddRange (Needs);
			list.Add(need);
			Needs = list.ToArray ();
		}

		public void AddActivity(Type activityType)
		{
			var activityInfo = new ActivityInfo (activityType);

			var list = new List<ActivityInfo> ();
			if (Activities != null)
				list.AddRange (Activities);
			list.Add(activityInfo);
			Activities = list.ToArray ();
		}

        static public GameLogic NewComplete(EngineSettings settings, ConsoleHelper console)
        {
            var logic = new GameLogic ();

            logic.AddEffect (new ThirstEffect (settings, console));
            logic.AddEffect (new HungerEffect (settings, console));
            logic.AddEffect (new DehydrationEffect (settings, console));
            logic.AddEffect (new StarvationEffect (settings, console));

            logic.AddNeed (new BuildShelterNeedIdentifier (settings, console));
            logic.AddNeed (new DrinkWaterNeedIdentifier (settings, console));
            logic.AddNeed (new EatFoodNeedIdentifier (settings, console));

            logic.AddActivity (typeof(BuildShelterActivity));
            logic.AddActivity (typeof(MillTimberActivity));
            logic.AddActivity (typeof(FellWoodActivity));
            logic.AddActivity (typeof(EatFoodActivity));
            logic.AddActivity (typeof(GatherFoodActivity));
            logic.AddActivity (typeof(CollectWaterActivity));
            logic.AddActivity (typeof(DrinkWaterActivity));

            return logic;
        }
	}
}

