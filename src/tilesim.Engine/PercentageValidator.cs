using System;

namespace tilesim.Engine
{
    static public class PercentageValidator
    {
        static public int Validate(int percentage)
        {
            if (percentage > 100)
                percentage = 100;

            if (percentage < 0)
                percentage = 0;

            return percentage;
        }

        static public decimal Validate(decimal percentage)
        {
            if (percentage > 100)
                percentage = 100;

            if (percentage < 0)
                percentage = 0;

            return percentage;
        }

        static public double Validate(double percentage)
        {
            if (percentage > 100)
                percentage = 100;

            if (percentage < 0)
                percentage = 0;

            return percentage;
        }
    }
}

