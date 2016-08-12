using System;
using datamanager.Data;

namespace tilesim.Engine
{
    public class OrderProcessor
    {
        public DataManager Data;

        public EngineContext Context;

        public OrderProcessor (EngineContext context, DataManager data)
        {
            Context = context;
            Data = data;
        }

        public void ProcessAll()
        {
            foreach (var order in Context.Orders) {
                order.Execute ();
            }
            Context.Orders.Clear ();
        }
    }
}

