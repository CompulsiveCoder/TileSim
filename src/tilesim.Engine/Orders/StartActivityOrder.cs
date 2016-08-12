using System;
using tilesim.Engine.Activities;
using tilesim.Engine.Entities;
using datamanager.Entities;
using System.Linq;
using System.Collections.Generic;

namespace tilesim.Engine
{
    [IndexType(typeof(BaseOrder))]
    public class StartActivityOrder : BaseOrder
    {
        public Person Person;

        public ActivityVerb Verb;

        public ItemType Type;

        public decimal Quantity;

        public StartActivityOrder (Person person, ActivityVerb verb, ItemType type, decimal quantity)
        {
            Person = person;
            Verb = verb;
            Type = type;
            Quantity = quantity;
        }

        public override void Execute ()
        {
            var activityInfos = (from a in Person.Tile.World.Logic.Activities
                                     where a.ItemType == Type
                                         && a.ActionType == Verb
                                     select a).ToArray ();
            
            var needEntry = new NeedEntry (
                Verb,
                Type,
                PersonVitalType.NotSet, // TODO: Make this dynamic
                Quantity,
                100); // TODO: Make this dynamic*/

            var activity = Person.Tile.World.Context.Engine.Persons.Decider.Creator.CreateActivity (Person, activityInfos [0].ActivityType, needEntry);

            Person.RushActivity (activity);
        }
    }
}

