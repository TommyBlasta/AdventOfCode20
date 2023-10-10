using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode20.Workers.Console
{
    public static class ScriptExecutor
    {
        public static async Task<(int AddressChange, int AccumulatorChange)> Execute(ConsoleScriptOperation operation)
        {
            switch (operation.Type)
            {
                case OperationType.Acc: 
                    {
                        return (1, operation.Argument);
                    }
                case OperationType.Jmp:
                    {
                        return (operation.Argument, 0);
                    }
                case OperationType.Nop:
                    {
                        return (1, 0);
                    }
                default:
                    {
                        throw new NotImplementedException("Operation not implemented!");
                    }
            }

        }
    }
}
