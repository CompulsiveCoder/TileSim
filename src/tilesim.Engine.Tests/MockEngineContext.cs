using System;
using tilesim.Engine.Entities;
using datamanager.Data;

namespace tilesim.Engine.Tests
{
	public class MockEngineContext : EngineContext
	{
		public MockEngineContext (EngineProcess process) : base(process)
		{
			throw new NotImplementedException ();
		}

		public MockEngineContext(EngineSettings settings, DataManager data) : base(settings, data)
		{
			
		}

		public new static EngineContext New()
		{
            var settings = EngineSettings.DefaultVerbose;

            settings.CycleDuration = 1; // Set the duration to 1 millisecond so there's no delay during tests

			return new MockGameCreator (settings).Create ();
		}
	}
}

