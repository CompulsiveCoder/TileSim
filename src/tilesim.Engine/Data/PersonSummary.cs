using System;
using Newtonsoft.Json;

namespace tilesim.Data
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

