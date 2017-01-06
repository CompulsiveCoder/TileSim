using System;
using System.Web;
using System.Web.UI;
using tilesim.Web;

namespace tilesim
{
    
    public partial class StartEmptyEngine : System.Web.UI.Page
    {
        void Page_Load(object sender, EventArgs e)
        {
            var speed = 1;
            // TODO: Remove or reimplement
            //var speed = Convert.ToInt32(Request.QueryString["speed"]);
            EngineWebHolder.Current.StartGame(speed);
            Response.Redirect("../Default.aspx?started=true");
        }
    }
}

