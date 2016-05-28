using System;
using tilesim.Engine.Entities;
using System.Collections.Generic;
using tilesim.Engine.Needs;

namespace tilesim.Engine.Activities
{
    [Activity(ActivityVerb.Fight)]
    [Serializable]
    public class FightActivity : BaseActivity
    {
        public FightActivity (Person person, NeedEntry needEntry, EngineSettings settings, ConsoleHelper console)
            : base(person, needEntry, settings, console)
        {
        }

        public override void Prepare (Person person)
        {
            throw new NotImplementedException ();
        }

        public override bool CheckFinished ()
        {
            throw new NotImplementedException ();
        }

        public override void Execute (Person person)
        {
            throw new NotImplementedException ();
        }

        public override bool CanAct (Person actor)
        {
            throw new NotImplementedException ();
        }
    }
}

