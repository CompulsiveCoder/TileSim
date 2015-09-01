using System;
using townsim.Entities;

namespace townsim.Data
{
	public class PeopleKeys
	{
		public string GetPersonKey(Guid personId)
		{
			return DataConfig.Prefix + "-Person-" + personId.ToString ();
		}

		/*public string GetPopulationKey(Guid townId)
		{
			return DataConfig.Prefix + "-Town-" + townId.ToString () + "-Population";
		}*/

		public string GetPeopleKey(Guid townId)
		{
			return DataConfig.Prefix + "-Town-" + townId.ToString () + "-People";
		}
	}
}

