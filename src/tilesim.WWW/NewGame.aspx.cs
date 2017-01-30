using System;
using System.Web;
using System.Web.UI;
using tilesim.Web;
using tilesim.Engine.Entities;
using tilesim.Engine;

namespace tilesim
{
    
    public partial class NewGame : System.Web.UI.Page
    {
        void Page_Load()
        {
            Console.WriteLine ("User requested a new game");

            var speed = Convert.ToInt32(Request.QueryString["speed"]);

            Console.WriteLine ("  Speed: " + speed);

            var settings = new EngineSettings ();

            settings.GameSpeed = speed;
            settings.OutputType = ConsoleOutputType.Debug;

            EngineWebHolder.Current.StartGame(settings);

            // TODO: Remove if not needed
            //Response.Redirect("Default.aspx");
        }
    }
}

