using System;
using System.Web;
using System.Web.UI;
using townsim.Engine;

namespace townsim
{

	public partial class LogPanel : System.Web.UI.UserControl
	{
		public Guid EngineId;

		public void Page_Load(object sender, EventArgs e)
		{
			EngineId = CurrentEngine.Id;
		}
	}
}

