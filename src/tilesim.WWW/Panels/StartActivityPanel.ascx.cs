using System;
using System.Web;
using System.Web.UI;
using tilesim.Web;
using tilesim.Engine;
using tilesim.Engine.Activities;
using tilesim.Engine.Entities;

namespace tilesim
{

    public partial class StartActivityPanel : System.Web.UI.UserControl
    {
        public void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString ["newactivity"] != null) {
                var newActivityName = Request.QueryString ["newactivity"];

                var amount = 1;
                if (Request.QueryString["amount"] != null)
                    amount = Convert.ToInt32(Request.QueryString ["amount"]);

                var parts = newActivityName.Split ('-');

                var verbString = parts [0];

                var typeString = parts [1];

                var verb = (ActivityVerb)Enum.Parse (typeof(ActivityVerb), verbString);

                var type = (ItemType)Enum.Parse (typeof(ItemType), typeString);

                var context = EngineWebHolder.Current.Context;

                var person = EngineWebHolder.Current.Context.Player;

                var order = new StartActivityOrder (person, verb, type, amount); // TODO

                EngineWebHolder.Current.Context.AddOrder (order);

            }
        }
    }
}

