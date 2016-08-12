using System;
using datamanager.Entities;

namespace tilesim.Engine
{
    public abstract class BaseOrder : BaseEntity
    {
        public BaseOrder ()
        {
        }

        public abstract void Execute();
    }
}

