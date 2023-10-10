using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode20.Workers
{
    public static class PassportParser
    {
        public static IEnumerable<string> GetSeparatePassports(string inputString)
        {
            var passPorts = inputString.Split("\n\r\n");
            return passPorts;
        }
    }
}
