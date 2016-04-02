using System;
using System.Web;
using System.Web.UI;
using townsim.Engine.Entities;
using townsim.Data;
using datamanager.Data;

namespace townsim
{

	public partial class PlayerPanel : System.Web.UI.UserControl
	{
		public Person Player;
		public Town Town;

		public void Page_Load(object sender, EventArgs e)
		{
			var towns = new DataManager().Get<Town>();
			if (towns.Length > 0)
			{
				Town = towns[0];
				if (Town.People.Length > 0)
					Player = Town.People[0];
			}
		}
	}
}

