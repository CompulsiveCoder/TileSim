using System;
using townsim.Data;
using System.Web;

namespace townsim.Engine
{
	public static class CurrentEngine
	{
		static public string Id { get; set; }
		/*static public string Id
		{
			get { return (string)HttpContext.Current.Session ["EngineId"]; }
			set { HttpContext.Current.Session ["EngineId"] = value; }
		}*/

		static public void Attach(string engineId)
		{
			Id = engineId;
			DataConfig.Prefix = "TownSim-" + engineId;
		}
	}
}

