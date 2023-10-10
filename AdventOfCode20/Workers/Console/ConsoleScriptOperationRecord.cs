using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode20.Workers.Console
{
    public struct ConsoleScriptOperationRecord
    {
        public OperationType Type { get; set; }
        public int Argument { get; set; }
    }
}
