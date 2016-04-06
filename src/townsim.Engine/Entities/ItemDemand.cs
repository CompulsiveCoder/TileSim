using System;
using townsim.Engine.Activities;
using townsim.Engine;
using townsim.Engine.Needs;

namespace townsim.Engine.Entities
{
    [Serializable]
    public class ItemDemand
    {
        public Person Person { get; set; }

        public ItemType Supply { get; set; }

        public decimal Amount { get;set; }

        public BaseActivity Activity { get; set; }

        public BaseGameEntity Target { get; set; }

        public ItemDemand (Person person, ItemType needType, decimal amount)
        {
            throw new NotImplementedException ();
            /*Person = person;
            Supply = needType;
            Amount = amount;*/
        }
    }
}

