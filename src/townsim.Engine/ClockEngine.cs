using System;

namespace townsim.Engine
{
	public class ClockEngine
	{
		public ClockEngine ()
		{
		}

		public DateTime GetGameTime(DateTime gameStartTime, DateTime now)
		{
			return DateTime.Now;
		}
	}
}

