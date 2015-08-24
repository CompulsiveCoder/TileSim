using System;
using NUnit.Framework;

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

