using System;

namespace tilesim.Engine.Tests
{
	public class MockGameEnvironment : GameEnvironment
	{
		// TODO: Remove if not needed
		/*public MockGameEnvironment () : base(new MockEngineContext())
		{
		}*/

		public MockGameEnvironment (EngineContext context) : base(context)
		{
		}
	}
}

