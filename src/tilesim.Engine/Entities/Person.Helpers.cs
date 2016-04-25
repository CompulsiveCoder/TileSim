using System;

namespace tilesim.Engine.Entities
{
	public partial class Person
	{
		public bool IsAdult
		{
			get { return Age >= 18; }
		}

		public bool IsChild
		{
			get { return !IsAdult; }
		}

		public bool CanWork
		{
			get { return IsAdult; }
		}

		public bool IsActive
		{
			get { return Activity != null; }
		}

		public bool IsHomeless { get { return Home == null || !Home.IsCompleted; } }
	}
}

