using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode20.Workers.Bags
{
    public class Bag
    {
        public string Name { get; set; }
        public IEnumerable<string> ContainedBags { get; set; }
    }
}
