using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class EngineClock
	{
		public DateTime StartTime;

		public TimeSpan RealDuration
		{
			get { return DateTime.Now.Subtract (StartTime); }
		}

		public TimeSpan GameDuration
		{
			get { return TimeSpan.FromSeconds (RealDuration.Seconds * Settings.GameSpeed); }
		}

		public EngineSettings Settings { get; set; }

		public EngineClock (DateTime startTime, EngineSettings settings)
		{
			Settings = settings;
			StartTime = startTime;
		}

		public EngineClock (EngineSettings settings)
		{
			Settings = settings;
			StartTime = DateTime.Now;
		}

		public string GetTimeSpanString(TimeSpan timeSpan)
		{
			string answer;
			if (timeSpan.TotalMinutes < 1.0)
			{
				answer = String.Format("{0}s", timeSpan.Seconds);
			}
			else if (timeSpan.TotalHours < 1.0)
			{
				answer = String.Format("{0}m:{1:D2}s", timeSpan.Minutes, timeSpan.Seconds);
			}
			else // more than 1 hour
			{
				answer = String.Format("{0}h:{1:D2}m:{2:D2}s", (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
			}

			return answer;
		}

		public string GetRealDurationString()
		{
			return GetTimeSpanString(DateTime.Now.Subtract (StartTime));
		}

		public string GetGameDurationString()
		{
			return GetTimeSpanString(GameDuration);
		}
	}
}

