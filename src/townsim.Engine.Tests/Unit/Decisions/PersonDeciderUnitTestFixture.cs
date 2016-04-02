using System;
using NUnit.Framework;
using townsim.Engine.Decisions;

namespace townsim.Engine.Tests
{
	[TestFixture]
	public class PersonDeciderUnitTestFixture
	{
		[Test]
		public void Test_()
		{
			var context = MockEngineContext.New ();

			var decider = new PersonDecider (context);

			//decider.
		}
	}
}

