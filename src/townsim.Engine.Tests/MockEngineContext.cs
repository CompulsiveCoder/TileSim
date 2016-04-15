using System;
using townsim.Engine.Entities;
using datamanager.Data;

namespace townsim.Engine.Tests
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

		// TODO: Remove if not needed
		/*public MockEngineContext ()
		{
			base(new MockEngineProcess(this));
		}*/

		public new static EngineContext New()
		{
			return new MockGameCreator (EngineSettings.DefaultVerbose).Create ();
		}
	}
}

