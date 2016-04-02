using System;
using townsim.Engine.Entities;
using System.Collections.Generic;
using townsim.Data;
using townsim.Engine.Needs;

namespace townsim.Engine.Decisions
{
	public class PersonDecider
	{
		public Random Randomizer = new Random();

		public EngineContext Context { get; set; }

		public ActivityCreator Creator { get;set; }

		public PersonDecider (EngineContext context)
		{
			Context = context;
			Creator = new ActivityCreator(Context.Settings);
		}

		public BaseActivity Decide(Person person)
		{
			if (Context.Settings.IsVerbose)
				Console.WriteLine ("    Deciding");
			
			var previousActivityName = person.ActivityName;

			var currentActivity = ChooseActivityBasedOnNeeds (person);

            if (currentActivity != null) {
                if (previousActivityName != currentActivity.GetType ().Name) {
                    if (Context.Settings.OutputType == ConsoleOutputType.Debug) {
                        Console.WriteLine ("    Player chose activity: " + currentActivity);
                    }
                }
            } else {
                if (Context.Settings.IsVerbose)
                    Console.WriteLine ("    Player is idle");
            }

            if (currentActivity == null)
                Console.WriteLine ("Activity: null");

			return currentActivity;
		}

		public bool PersonIsHomeless(Person person)
		{
			return person.Home == null
				|| !person.Home.IsCompleted;
		}

		public BaseActivity ChooseActivityBasedOnNeeds(Person person)
		{
			if (Context.Settings.IsVerbose)
				Console.WriteLine ("    Choosing activity based on needs");
			
			var entries = GetHighestPriorities (person);

			var noEntriesFound = (entries.Length == 0);

			if (noEntriesFound)
			{
				if (Context.Settings.IsVerbose)
					Console.WriteLine ("    No needs found. Doing nothing.");
				
				//person.ClearActivity(); // TODO: Should a random activity be chosen?

				return null;
			}
			else {
				var index = RandomlySelectEntryIndex (entries.Length);

				var activity = ChooseActivityBasedOnNeed (person, entries [index]);

				return activity;
			}
		}

		public BaseActivity ChooseActivityBasedOnNeed(Person person, NeedEntry needEntry)
		{
			if (Context.Settings.IsVerbose) {
				Console.WriteLine ("    Choosing activity based on need for " + needEntry.Type);
			}

			var possibleActivities = new List<ActivityInfo> ();

			foreach (var activityInfo in Context.World.Logic.Activities) {
				if (activityInfo.IsSuited (needEntry.Type)) {
					possibleActivities.Add (activityInfo);
				}
			}

			// TODO: Add support for randomly choosing from multiple possibilities
			if (possibleActivities.Count > 1)
				throw new NotSupportedException ("Multiple activities identified. That's not yet supported.");

			if (possibleActivities.Count == 0)
				throw new Exception ("No activities found to address the need for " + needEntry.Type + ". Ensure the activities have been added to the environment logic.");
			

			if (Context.Settings.IsVerbose) {
				Console.WriteLine ("    Activity chosen: " + possibleActivities [0].ActivityType.Name);
			}

			var activity = Creator.CreateActivity(person, possibleActivities[0].ActivityType, needEntry);

			return activity;


			// TODO: Remove if not needed
		/*	var previousActivity = person.ActivityType;

			if (Context.Settings.OutputType == ConsoleOutputType.Debug
				&& person.Id == Context.Settings.PlayerId) {
				Console.WriteLine ("Need: " + needType.ToString ());
			}

			if (needType == NeedType.Shelter) {

				if (person.HasDemand(NeedType.Timber))
				{
					if (person.HasDemand(NeedType.Wood))
						new WoodDecision (Context).Decide (person);
					else
						new TimberDecision (Context).Decide (person);
				}
				else
					new ShelterDecision ().Decide (person);
			}
			else if (needType == NeedType.Water)
			{
				new WaterDecision (Context).Decide (person);
			}
			else if (needType == NeedType.Food)
			{
				new FoodDecision (Context).Decide (person);
			}

			var activityHasChanged = previousActivity != person.ActivityType;

			if (activityHasChanged && Context.Settings.PlayerId == person.Id) {
				switch (person.ActivityType) {
				case ActivityType.CollectingWater:
					Context.Log.WriteLine ("The player has started collecting water.");
					break;
				case ActivityType.Drinking:
					Context.Log.WriteLine("The player has started drinking water.");
					break;
				case ActivityType.FellWood:
					Context.Log.WriteLine ("The player has started felling wood.");
					break;
				case ActivityType.MillTimber:
					Context.Log.WriteLine ("The player has started milling timber.");
					break;
				}
			}*/
		}

		public int RandomlySelectEntryIndex(int length)
		{
			var index = 0;

			if (length == 1) {
				index = 0;
			} else {
				index = new Random ().Next (0, length);
			}

			return index;
		}

		public NeedEntry[] GetHighestPriorities(Person person)
		{
			if (Context.Settings.IsVerbose)
				Console.WriteLine ("      Getting highest priorities");


			var possibleChoices = new List<NeedEntry> ();
			var highestValue = 0m;

			foreach (var entry in person.Needs) {
				if (entry.Priority > 0) {
					if (entry.Priority == highestValue) {
						possibleChoices.Add (entry);
					}
					if (entry.Priority > highestValue) {
						highestValue = entry.Priority;
						possibleChoices.Clear ();
						possibleChoices.Add (entry);
					}
				}
			}

			if (Context.Settings.IsVerbose) {
				foreach (var entry in possibleChoices) {
					Console.WriteLine ("      " + entry.Type + " - " + entry.Priority);
				}
			}

			return possibleChoices.ToArray ();

/*			var possibleChoices = new List<PriorityTypes> ();
			var highestValue = 0m;

			foreach (var priority in person.Priorities) {
				if (priority.Value > 0) {
					if (priority.Value == highestValue) {
						possibleChoices.Add (priority.Key);
					}
					if (priority.Value > highestValue) {
						highestValue = priority.Value;
						possibleChoices.Clear ();
						possibleChoices.Add (priority.Key);
					}
				}
			}

			return possibleChoices.ToArray ();*/
		}
	}
}

