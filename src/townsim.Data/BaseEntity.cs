using System;
using System.Web.Script.Serialization;

namespace townsim.Data
{
	public class BaseEntity
	{
		public Guid Id { get; set; }

		public BaseEntity ()
		{
		}

		public string ToJson()
		{
			return new JavaScriptSerializer ().Serialize (this);
		}
	}
}

