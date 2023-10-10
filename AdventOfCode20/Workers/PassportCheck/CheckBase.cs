using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode20.Workers.PassportCheck
{
    public abstract class CheckBase
    {
        protected bool IsInRangeInclusive(int value, int min, int max)
        {
            return value <= max && value >= min;
        }
    }
}
