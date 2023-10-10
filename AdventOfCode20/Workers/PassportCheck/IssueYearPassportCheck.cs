using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode20.Workers.PassportCheck
{
    public class IssueYearPassportCheck : PassportCheckAbstract
    {
        public override string Property => "iyr";

        protected override bool CheckProperty(string propertyValue)
        {
            //1
            //1
            var hasFourDigits = propertyValue.Length == 4;

            //2
            var value = int.Parse(propertyValue);
            var inValidRange = IsInRangeInclusive(value, 2010, 2020);

            return hasFourDigits && inValidRange;
        }
    }
}
