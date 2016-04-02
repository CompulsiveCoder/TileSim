using System;
using datamanager.Entities;

namespace townsim.Engine.Entities
{
	public interface IActivityTarget
	{
		Person[] People { get;set; }
	}
}

