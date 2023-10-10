using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode20.Workers.PassportCheck
{
    public class EyeColorPassportCheck : PassportCheckAbstract
    {
        public override string Property => "ecl";

        private string[] AllowedValues = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        protected override bool CheckProperty(string propertyValue)
        {
            return AllowedValues.Contains(propertyValue);
        }
    }
}
