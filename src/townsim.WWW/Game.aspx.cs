using System;
using System.Web;
using System.Web.UI;
using townsim.Engine;

namespace townsim
{
	
	public partial class Game : System.Web.UI.Page
	{
		public Guid EngineId = Guid.Empty;

		public void Page_Load(object sender, EventArgs e)
		{
			var idString = Request.QueryString["engineId"];
			if (!String.IsNullOrEmpty(idString))
			{
				EngineId = Guid.Parse(idString);	
				if (EngineId != Guid.Empty)
					CurrentEngine.StartThread(EngineId);
			}
			else
			{
				if (CurrentEngine.Id != Guid.Empty)
					EngineId = CurrentEngine.Id;
			}
		}
	}
}

