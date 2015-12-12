using System;
using townsim.Entities;

namespace townsim.Engine.Activities
{
	[Serializable]
	public abstract class BaseActivity
	{
		public EngineSettings Settings { get; set; }

		public EngineClock Clock { get; set; }

		public Person Person { get; set; }

		public IActivityTarget Target { get;set; }

		public BaseActivity ()
		{
		}

		public BaseActivity (Person person, EngineSettings settings)
		{
			Settings = settings;

			Init (person);
		}

		public BaseActivity (Person person, EngineSettings settings, EngineClock clock)
		{
			Settings = settings;
			Clock = clock;

			Init (person);
		}

		public virtual void Init(Person person)
		{
			if (Settings.OutputType == ConsoleOutputType.General)
				Console.WriteLine ("Starting activity: " + person.ActivityType);

			person.Activity = this;
			Person = person;
		}

		public void StartSingleCycle()
		{
			if (Person.ActivityTarget == null)
				Person.ActivityTarget = Target;

			if (IsComplete ()) {
				Finish ();
				CleanUp ();
			} else if (IsImpossible ())
				CleanUp ();
			else
				ExecuteSingleCycle ();
		}

		public abstract void ExecuteSingleCycle();

		public abstract void Start();

		public abstract void Finish();

		public abstract bool IsComplete();

		public abstract bool IsImpossible();

		public void CleanUp()
		{
			Person.Activity = null;
			Person.ActivityType = ActivityType.Inactive;
			Person.ActivityData.Clear ();
		}

		public void SetTarget(IActivityTarget target)
		{
			Target = target;
			Person.ActivityTarget = target;
		}

		public void ClearTarget()
		{
			Target = null;
			Person.ActivityTarget = null;
		}

		public void Cancel()
		{
			CleanUp();
		}
	}
}

