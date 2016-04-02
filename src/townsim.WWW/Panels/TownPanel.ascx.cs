using System;
using System.Web;
using System.Web.UI;
using townsim.Data;
using townsim.Engine.Entities;
using datamanager.Data;

namespace townsim
{

	public partial class TownPanel : System.Web.UI.UserControl
	{
		public Town Town { get;set; }

		public void Page_Load(object sender, EventArgs e)
		{
			//var id = new Guid(Request.QueryString["id"]);
			/*var reader = new TownReader();
		var id = Guid.Empty;
		throw new Exception(TownId);
		if (!String.IsNullOrEmpty(TownId))
		{
			id = Guid.Parse(TownId);

			Town = reader.Read(id);
		}
		else
			Town = new Town();*/

			Town = new DataManager().Get<Town>()[0];
			//Town = new Town();
		}
	}
}

