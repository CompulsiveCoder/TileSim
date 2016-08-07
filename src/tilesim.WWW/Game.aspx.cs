using System;
using System.Web;
using System.Web.UI;
using tilesim.Engine;

namespace tilesim
{
	
	public partial class Game : System.Web.UI.Page
	{
		public string EngineId = String.Empty;

		public void Page_Load(object sender, EventArgs e)
        {
            /*var id = Request.QueryString["engineId"];
            var speed = Convert.ToInt32(Request.QueryString["speed"]);
			if (!String.IsNullOrEmpty(id))
			{
				EngineId = id;	
				EngineWebHolder.Current.StartThread(EngineId, speed);
			}*/
		}
	}
}

