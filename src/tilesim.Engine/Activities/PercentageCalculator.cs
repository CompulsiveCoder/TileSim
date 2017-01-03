using System;

namespace tilesim.Engine.Activities
{
    public class PercentageCalculator
    {
        public decimal TotalUnits = 0;

        public PercentageCalculator()
        {
        }

        public decimal Calculate(decimal totalUnits, int position)
        {
            var unitFraction = 1m / totalUnits;

            var percentage = unitFraction * position * 100;

            return percentage;
        }
    }
}

