using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode20.Workers.PassportCheck
{
    public class BirthYearPassportCheck : PassportCheckAbstract
    {
        public override string Property => "byr";

        protected override bool CheckProperty(string propertyValue)
        {
            //1
            var hasFourDigits = propertyValue.Length == 4;

            //2
            var value = int.Parse(propertyValue);
            var inValidRange = IsInRangeInclusive(value, 1920, 2002);

            return hasFourDigits && inValidRange;
        }
    }
}
