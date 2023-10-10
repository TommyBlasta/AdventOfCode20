using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode20.Workers.PassportCheck
{
    public class HairColorPassportCheck : PassportCheckAbstract
    {
        public override string Property => "hcl";

        protected override bool CheckProperty(string propertyValue)
        {
            //a # followed by exactly six characters 0-9 or a-f.
            var regex = new Regex("^#[a-f0-9]{6}$");
            var match = regex.Match(propertyValue);
            return match.Success;
        }
    }
}
