using System;

namespace townsim.Alerts
{
	public class BaseAlert
	{
		public string Message { get;set; }

		public DateTime TimeStamp { get; set; }

		public BaseAlert (string message)
		{
			Message = message;
			TimeStamp = DateTime.Now;
		}

		public override string ToString ()
		{
			return Message;
		}
	}
}

