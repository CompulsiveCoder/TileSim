using System;
using Newtonsoft.Json;

namespace townsim.Data
{
	[JsonObject("Person")]
	public class PersonSummary
	{
		public Guid Id { get; set; }

		public PersonSummary ()
		{
		}
	}
}

