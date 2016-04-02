using System;
using townsim.Engine.Entities;

namespace townsim.Engine
{
	[Serializable]
	public class EngineClock
	{
		public DateTime StartTime;

		public TimeSpan RealDuration
		{
			get { return DateTime.Now.Subtract (StartTime); }
		}

		public TimeSpan GameDuration
		{
      		get {
				return TimeSpan.FromSeconds (RealDuration.TotalSeconds * Settings.GameSpeed);
			}
		}

		public EngineSettings Settings { get; set; }

		public EngineClock (DateTime startTime, EngineContext context)
		{
			throw new NotImplementedException ();
			//Context = context;
			//StartTime = startTime;
		}

		public EngineClock (EngineSettings settings)
		{
			if (settings.IsVerbose)
				Console.WriteLine ("Constructing game engine clock");
			
			Settings = settings;
			StartTime = DateTime.Now;

			if (settings.IsVerbose)
				Console.WriteLine ("Setting start time to: " + StartTime.ToString());
		}

		public string GetTimeSpanString(TimeSpan timeSpan)
		{
			string answer;
			if (timeSpan.TotalMinutes < 1.0) {
				answer = String.Format ("{0}s", timeSpan.Seconds);
			} else if (timeSpan.TotalHours < 1.0) {
				answer = String.Format ("{0}m:{1:D2}s", timeSpan.Minutes, timeSpan.Seconds);
			} else if (timeSpan.TotalDays < 1) {
				answer = String.Format ("{0}h:{1:D2}m:{2:D2}s", (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
			} else if (timeSpan.TotalDays < 30) {
				answer = String.Format ("{0}days, {1}h:{2:D2}m:{3:D2}s", timeSpan.Days, (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
			} else {
				answer = String.Format ("{0}months, {1}days, {2}h:{3:D2}m:{4:D2}s", (int)(timeSpan.TotalDays / 30), timeSpan.TotalDays, (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
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

		public string GetSpeedComparisonString()
		{
			throw new NotImplementedException ();
			//return GetTimeSpanString (TimeSpan.FromSeconds (Settings.GameSpeed));
		}

		public static EngineClock Default
		{
			get { return new EngineClock (EngineSettings.Default); }
		}
	}
}

