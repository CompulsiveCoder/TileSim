using System;
using townsim.Data;
using System.Threading;
using townsim.Engine.Entities;
using System.Collections.Generic;

namespace townsim.Engine
{
	public class PlayerDiedException : Exception
	{
        // TODO: Output some info about the player
        public PlayerDiedException(Person person) : base("The player died at " + person.Age)
		{
		}
	}

}

