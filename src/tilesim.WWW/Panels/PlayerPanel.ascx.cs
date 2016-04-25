using System;
using System.Web;
using System.Web.UI;
using tilesim.Engine.Entities;
using tilesim.Data;
using datamanager.Data;
using tilesim.Engine;

namespace tilesim
{

	public partial class PlayerPanel : System.Web.UI.UserControl
	{
		public Person Player;
		public GameTile Tile;

		public void Page_Load(object sender, EventArgs e)
		{
            Player = CurrentEngine.Context.Player;
			/*var tiles = new DataManager().Get<Tile>();
			if (tiles.Length > 0)
			{
				Tile = tiles[0];
				if (Tile.People.Length > 0)
					Player = Tile.People[0];
			}*/
		}
	}
}

