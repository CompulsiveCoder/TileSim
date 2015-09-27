using System;
using System.Web;
using System.Web.UI;
using townsim.Entities;
using townsim.Data;

namespace townsim
{

	public partial class PlayerPanel : System.Web.UI.UserControl
	{
		public Person Player;
		public Town Town;

		public void Page_Load(object sender, EventArgs e)
		{
			var indexer = new TownIndexer();
			var towns = indexer.Get();
			if (towns.Length > 0)
			{
				Town = towns[0];
				if (Town.People.Length > 0)
					Player = Town.People[0];
			}
		}
	}
}

