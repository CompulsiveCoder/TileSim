using System;
using NUnit.Framework;
using datamanager.Data;

namespace townsim.Data.Tests
{
	public class BaseTestFixture
	{
		public BaseTestFixture ()
		{
		}

		[SetUp]
		public void Initialize()
		{
			DataConfig.Prefix += "-Test";
		}
	}
}

