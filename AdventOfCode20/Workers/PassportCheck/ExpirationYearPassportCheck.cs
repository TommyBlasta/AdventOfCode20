using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode20.Workers.PassportCheck
{
    public class ExpirationYearPassportCheck : PassportCheckAbstract
    {
        public override string Property => "eyr";

        protected override bool CheckProperty(string propertyValue)
        {
            //1
            var hasFourDigits = propertyValue.Length == 4;

            //2
            var value = int.Parse(propertyValue);
            var inValidRange = IsInRangeInclusive(value, 2020, 2030);

            return hasFourDigits && inValidRange;
        }
    }
}
