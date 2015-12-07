using System;
using townsim.Entities;
using datamanager.Data;

namespace townsim.Data
{
	public class PeopleKeys
	{
		public string GetPersonKey(Guid personId)
		{
			return DataConfig.Prefix + "-Person-" + personId.ToString ();
		}

		public string GetPeopleKey(Guid townId)
		{
			return DataConfig.Prefix + "-Town-" + townId.ToString () + "-People";
		}
	}
}

