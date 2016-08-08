using System;
using System.Web;
using System.Web.UI;
using tilesim.Web;

namespace tilesim
{
    
    public partial class NewGame : System.Web.UI.Page
    {
        void Page_Load()
        {
            var speed = Convert.ToInt32(Request.QueryString["speed"]);
            EngineWebHolder.Current.StartGame(speed);
            Response.Redirect("Default.aspx");
        }
    }
}

