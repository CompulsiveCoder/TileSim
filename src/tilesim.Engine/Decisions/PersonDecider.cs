using System;
using tilesim.Engine.Entities;
using System.Collections.Generic;
using tilesim.Engine.Needs;

namespace tilesim.Engine.Decisions
{
	public class PersonDecider
	{
		public Random Randomizer = new Random();

		public EngineContext Context { get; set; }

		public ActivityCreator Creator { get;set; }

		public PersonDecider (EngineContext context)
		{
			Context = context;
            Creator = new ActivityCreator(Context.Settings, Context.Console);
		}

		public BaseActivity Decide(Person person)
		{
			if (Context.Settings.IsVerbose)
                Context.Console.WriteDebugLine ("    Deciding");
			
			var previousActivityName = person.ActivityText;

			var currentActivity = ChooseActivityBasedOnNeeds (person);

            if (currentActivity != null) {
                if (previousActivityName != currentActivity.GetType ().Name) {
                    if (Context.Settings.OutputType == ConsoleOutputType.Debug) {
                        Context.Console.WriteDebugLine ("    Player chose activity: " + currentActivity);
                    }
                }
            } else {
                if (Context.Settings.IsVerbose)
                    Context.Console.WriteDebugLine ("    Player is idle");
            }

            if (currentActivity == null)
                Context.Console.WriteDebugLine ("Activity: null");

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
                Context.Console.WriteDebugLine ("    Choosing activity based on needs");
			
			var entries = GetHighestPriorities (person);

			var noEntriesFound = (entries.Length == 0);

			if (noEntriesFound)
			{
				if (Context.Settings.IsVerbose)
                    Context.Console.WriteDebugLine ("    No needs found. Doing nothing.");
				
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
                Context.Console.WriteDebugLine ("    Choosing activity based on need for " + needEntry.ItemType);
			}

			var possibleActivities = new List<ActivityInfo> ();

			foreach (var activityInfo in Context.World.Logic.Activities) {
                if (activityInfo.IsSuited (needEntry.ActionType, needEntry.ItemType)) {
					possibleActivities.Add (activityInfo);
				}
			}

			// TODO: Add support for randomly choosing from multiple possibilities
			if (possibleActivities.Count > 1)
				throw new NotSupportedException ("Multiple activities identified. That's not yet supported.");

			if (possibleActivities.Count == 0)
				throw new Exception ("No activities found to address the need for " + needEntry.ItemType + ". Ensure the activities have been added to the environment logic.");
			

			if (Context.Settings.IsVerbose) {
                Context.Console.WriteDebugLine ("    Activity chosen: " + possibleActivities [0].ActivityType.Name);
			}

            var activity = GetActivity (person, possibleActivities [0].ActivityType, needEntry);

			return activity;
		}

        public BaseActivity GetActivity(Person person, Type activityType, NeedEntry needEntry)
        {
            var activity = person.GetActivity (activityType);
            if (activity == null)
                activity = Creator.CreateActivity(person, activityType, needEntry);

            return activity;
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
                Context.Console.WriteDebugLine ("      Getting highest priorities");


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
                    Context.Console.WriteDebugLine ("      " + entry.ActionType + " " + entry.ItemType + ": " + entry.Priority);
				}
			}

			return possibleChoices.ToArray ();

		}
	}
}

