using System;
using System.Web;
using System.Web.UI;
using tilesim.Web;

namespace tilesim.WWW
{
    
    public partial class ContinueGame : System.Web.UI.Page
    {
        public void Page_Load(object sender, EventArgs e)
        {
            Console.WriteLine ("User is continuing an existing game");

            var gameId = Request.QueryString["id"];

            Console.WriteLine ("  Game ID: " + gameId);
            /*
            var settings = new EngineSettings ();

            settings.GameSpeed = speed;
            settings.OutputType = ConsoleOutputType.Debug;


            Response.Redirect("Default.aspx");*/

            EngineWebHolder.Current.ContinueGame(gameId);
        }
    }
}

