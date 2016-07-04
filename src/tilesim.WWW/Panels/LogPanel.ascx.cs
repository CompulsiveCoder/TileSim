using System;
using System.Web;
using System.Web.UI;
using tilesim.Engine;

namespace tilesim
{

	public partial class LogPanel : System.Web.UI.UserControl
	{
		public string EngineId;

		public void Page_Load(object sender, EventArgs e)
		{
			//EngineId = EngineHolder.Id;
		}
	}
}

