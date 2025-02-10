using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    public static class Utilities
    {
        public static bool IsZeroOrPositive(double value)
        {
            bool valid = true; // this method assumes that the value is correct
            if (value < 0.0)
                valid = false; //the test has determind that the value is not correct
            return valid;
        }

        public static bool IsZeroOrPositive(int value)
        {
            bool valid = true;
            if (value < 0)
                valid = false;
            return valid;
        }

        public static bool IsZeroOrPositive(decimal value)
        {
            bool valid = true;
            if (value < 0.0m)
                valid = false;
            return valid;
        }
    }
}
