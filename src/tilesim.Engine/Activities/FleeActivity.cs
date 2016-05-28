using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine.Activities
{
    [Serializable]
    [Activity(ActivityVerb.Flee)]
    public class FleeActivity : BaseActivity
    {
        public FleeActivity (Person actor, NeedEntry needEntry, EngineSettings settings, ConsoleHelper console)
            : base(actor, needEntry, settings, console)
        {
        }

        public override void Prepare (Person person)
        {
            throw new NotImplementedException ();
        }

        public override void Execute (Person person)
        {
            Status = "Fleeing";

            throw new NotImplementedException ();
        }

        public override bool CheckFinished ()
        {
            throw new NotImplementedException ();
        }

        public override void ConfirmProduced (NeedEntry entry)
        {
            throw new NotImplementedException ();
        }

        public override bool CanAct (Person actor)
        {
            throw new NotImplementedException ();
        }
    }
}

