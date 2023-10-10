using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode20.Workers.PassportCheck
{
    public abstract class PassportCheckAbstract : CheckBase
    {
        public abstract string Property { get; }
        protected abstract bool CheckProperty(string propertyValue);
        public bool Check(string propertyValue)
        {
            return CheckProperty(propertyValue);
        }
    }
}
