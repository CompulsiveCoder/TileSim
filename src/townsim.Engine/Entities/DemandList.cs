using System;
using townsim.Engine.Entities;
using System.Collections.Generic;
using System.Linq;

namespace townsim.Engine
{
    // TODO: Come up with a better name for this class
    [Serializable]
    public class DemandList
    {
        public List<ItemDemand> Demands = new List<ItemDemand>();

        public Person Person { get;set; }

        public DemandList (Person person)
        {
            Person = person;
        }

        public decimal GetDemandAmount(ItemType needType)
        {
            return (from demand in Demands
                where demand.Supply == needType
                select demand.Amount).Sum ();
        }

        public bool HasDemand(ItemType needType)
        {
            var totalDemand = GetDemandAmount(needType);

            return totalDemand > 0;
        }

        public void AddDemand(ItemType supply, decimal amount)
        {
            Demands.Add (new ItemDemand (Person, supply, amount));
        }

        public void RemoveDemand(ItemType supply, decimal amountToRemove)
        {
            var totalRemoved = 0.0m;

            var amountRemainingToRemove = amountToRemove;

            while (totalRemoved < amountToRemove
                && HasDemand(supply)) {
                var demandsFound = (from d in Demands
                    where d.Supply == supply
                    && d.Amount > 0
                    select d).ToArray();

                if (demandsFound.Length > 0) {
                    var demandFound = demandsFound [0];

                    if (demandFound.Amount > amountToRemove) {
                        demandFound.Amount -= amountRemainingToRemove;

                        totalRemoved += amountToRemove;
                    }
                    else {
                        Demands.Remove (demandFound);
                        amountRemainingToRemove -= demandFound.Amount;

                        totalRemoved += demandFound.Amount;
                    }
                }
            }
        }
    }
}

