using System;
using townsim.Entities;
using Newtonsoft.Json;

namespace townsim.Engine.Activities
{
	[Serializable]
	public abstract class BaseActivityOld
	{
		[JsonIgnore]
		[NonSerialized]
		public EngineContext Context;

		public Person Person { get; set; }

		public IActivityTarget Target { get;set; }

		public ActivityType Type { get;set; }

		public bool IsStarted = false;

		public bool IsComplete
		{
			get {
				var isComplete = CheckComplete ();
				if (isComplete)
					Console.WriteLine ("Activity complete.");
				return isComplete;
			}
		}

		public bool IsImpossible
		{
			get {
				var isImpossible = CheckImpossible ();
				if (isImpossible)
					Console.WriteLine ("Activity is impossible.");
				return isImpossible;
			}
		}

		public BaseActivityOld ()
		{
		}

		public BaseActivityOld (ActivityType activityType, Person person, EngineContext context)
		{
			Construct (activityType, person, context);
		}

		public virtual void Construct(ActivityType activityType, Person person, EngineContext context)
		{
			Context = context;
			Type = activityType;

			AssignPerson (person);

			if (Context.Settings.OutputType == ConsoleOutputType.Debug)
				Console.WriteLine ("Initializing activity: " + person.ActivityType);
		}

		public virtual void AssignPerson(Person person)
		{
			if (person.Activity == null) // Check for null first, to avoid stack overflow
				person.Assign(this);
			Person = person;
		}

		public void StartSingleCycle()
		{
			if (Type == ActivityType.Inactive)
				throw new Exception ("The activity type has not been set on activity: " + this.GetType ().Name);

			if (Person.ActivityType == Type) {
				if (IsComplete) {
					Finish ();
					CleanUp ();
				} else if (IsImpossible) {
					Cancel ();
					CleanUp ();
				} else {
					ExecuteSingleCycle ();

					// Check again whether it's complete or impossible
					/*if (IsComplete) {
						Finish ();
						CleanUp ();
					} else if (IsImpossible) {
						Cancel ();
						CleanUp ();
					}*/
				}
			}
		}

		protected abstract void ExecuteSingleCycle();

		public void RunCycles(int numberOfCyclesToRun)
		{
			for (int i = 0; i < numberOfCyclesToRun; i++){
				StartSingleCycle ();
			}
		}

		public abstract void Start();

		public abstract void Finish();

		public abstract bool CheckComplete();

		public abstract bool CheckImpossible();

		public virtual void CleanUp()
		{
			Person.ClearActivity ();
		}

		public void SetTarget(IActivityTarget target)
		{
			Target = target;
		}

		public void ClearTarget()
		{
			Target = null;
		}

		public void Cancel()
		{
			CleanUp();
		}
	}
}

