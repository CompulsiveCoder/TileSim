using System;
using datamanager.Entities;

namespace townsim.Entities
{
	public interface IActivityTarget
	{
		Person[] People { get;set; }
	}
}

