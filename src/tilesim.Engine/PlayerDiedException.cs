using System;
using System.Threading;
using tilesim.Engine.Entities;
using System.Collections.Generic;

namespace tilesim.Engine
{
	public class PlayerDiedException : Exception
	{
        // TODO: Output some info about the player
        public PlayerDiedException(Person person) : base("The player died at " + person.Age)
		{
		}
	}

}

