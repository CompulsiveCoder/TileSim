using System;
using tilesim.Engine.Entities;
using tilesim.Engine.Activities;
using tilesim.Engine.Decisions;

namespace tilesim.Engine
{
	public class PersonEngine
	{
		public EngineContext Context { get;set; }

		public PersonDecider Decider { get; set; }

		public PersonEngine (EngineContext context)
		{
			Context = context;
			Decider = new PersonDecider (context);
		}

		public void StartSingleCycle(Person person)
		{
			if (Context.Settings.IsVerbose)
				Context.Console.WriteDebugLine ("Starting cycle for person");

			RegisterNeeds (person);

			ChooseActivity (person);

			PerformActivity (person);
		}

		public void RegisterNeeds(Person person)
		{
			if (Context.Settings.IsVerbose)
				Context.Console.WriteDebugLine ("  Registering needs for person");

			foreach (var need in Context.World.Logic.Needs) {
				need.RegisterIfNeeded (person);
			}
		}

		public void ChooseActivity(Person person)
		{
			if (Context.Settings.IsVerbose)
                Context.Console.WriteDebugLine ("  Making decisions for person");
			
			var activity = Decider.Decide (person);

            if (activity != null) {
                var userIsAlreadyPerformingActivity = (activity.Name == person.ActivityName);

                if (!userIsAlreadyPerformingActivity)
                    person.RushActivity (activity);
            }
		}

		public void PerformActivity(Person person)
		{
			var activity = person.Activity;

			if (Context.Settings.IsVerbose)
                Context.Console.WriteDebugLine ("  Performing activity: " + (activity != null ? activity.GetType().Name : "[idle]"));

			if (activity != null)
				activity.Act (person);
		}
	}
}

