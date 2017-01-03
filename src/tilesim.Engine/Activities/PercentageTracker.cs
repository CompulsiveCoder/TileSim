using System;

namespace tilesim.Engine.Activities
{
    public class PercentageTracker
    {
        public decimal TotalUnits = 0;

        public int CurrentPosition = 0;

        public PercentageCalculator Calculator = new PercentageCalculator();

        public int LastPositionWritten = 0;
        public int PositionWriteInterval = 10;

        public PercentageTracker (decimal totalUnits)
        {
            TotalUnits = totalUnits;
            PositionWriteInterval = (int)(TotalUnits / 1000);
            CurrentPosition = 1;
        }

        public void Next()
        {
            CurrentPosition++;
        }

        public decimal CalculatePercentage()
        {
            return Calculator.Calculate (TotalUnits, CurrentPosition);
        }

        public void ConsoleWritePercentage()
        {
            if (CurrentPosition > LastPositionWritten + PositionWriteInterval) {
                var percentage = CalculatePercentage ();
                Console.WriteLine ("");
                Console.WriteLine (percentage + "%");
                LastPositionWritten = CurrentPosition;
            }
        }
    }
}

