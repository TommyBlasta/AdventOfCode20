using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode20.Workers.Console
{
    public class RunResult
    {
        public bool Finished { get; set; }
        public int LastAddress { get; set; }
        public int AccumulatorValue { get; set; }
        public IEnumerable<int> RanAddresses { get; set; }  
    }
}