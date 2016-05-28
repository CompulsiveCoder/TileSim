using System;

namespace tilesim.Engine.Activities
{
    public enum ActivityVerb
    {
        NotSet,

        // Fight/flee
        Fight,
        Flee,

        // Energy
        Sleep,

        // Food and drink
        Eat,
        Drink,
        Gather,

        // Timber
        Fell,
        Mill,

        // Construction
        Build
    }
}

