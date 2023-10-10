using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode20.Workers.PassportCheck
{
    public class PassportIdCheck : PassportCheckAbstract
    {
        public override string Property => "pid";

        protected override bool CheckProperty(string propertyValue)
        {
            var regex = new Regex("^[0-9]{9}$");
            var match = regex.Match(propertyValue);
            return match.Success;
        }
    }
}
