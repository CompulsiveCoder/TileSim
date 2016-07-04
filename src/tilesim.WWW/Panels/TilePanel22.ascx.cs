using System;
using System.Web;
using System.Web.UI;
using tilesim.Data;
using tilesim.Engine.Entities;
using datamanager.Data;

namespace tilesim
{

	public partial class TilePanel2 : System.Web.UI.UserControl
	{
		public Tile Tile { get;set; }

		public void Page_Load(object sender, EventArgs e)
		{
			//var id = new Guid(Request.QueryString["id"]);
			/*var reader = new TileReader();
		var id = Guid.Empty;
		throw new Exception(TileId);
		if (!String.IsNullOrEmpty(TileId))
		{
			id = Guid.Parse(TileId);

			Tile = reader.Read(id);
		}
		else
			Tile = new Tile();*/

			Tile = new DataManager().Get<Tile>()[0];
			//Tile = new Tile();
		}
	}
}

