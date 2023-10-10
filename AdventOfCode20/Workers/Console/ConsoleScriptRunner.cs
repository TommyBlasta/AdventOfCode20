using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode20.Workers.Console
{
    public class ConsoleScriptRunner
    {
        private readonly Dictionary<string, OperationType> stringOperationMapping = new Dictionary<string, OperationType>()
        {
            { "nop", OperationType.Nop },
            { "acc", OperationType.Acc },
            { "jmp", OperationType.Jmp }

        };

        public async Task<RunResult> Run(ConsoleScriptOperation[] operations, CancellationToken cancellationToken = default)
        {
            var ranAddresses = new HashSet<int>();

            var currentAddress = 0;
            var accumulator = 0;

            while (currentAddress != operations.Count())
            {
                var changes = await ScriptExecutor.Execute(operations[currentAddress]);
                ranAddresses.Add(currentAddress);
                var newAddress = currentAddress + changes.AddressChange;

                if (ranAddresses.Contains(newAddress))
                {
                    return new RunResult()
                    {
                        Finished = false,
                        LastAddress = currentAddress,
                        AccumulatorValue = accumulator,
                        RanAddresses = ranAddresses
                    };
                }

                currentAddress = newAddress;
                accumulator += changes.AccumulatorChange;
            }

            return new RunResult()
            {
                Finished = true,
                LastAddress = currentAddress,
                AccumulatorValue = accumulator,
                RanAddresses = ranAddresses
            };
        }
        public async Task<RunResult> Run(string script, CancellationToken cancellationToken = default)
        {
            var operations = (await GetOperationsFromScript(script, cancellationToken)).ToArray();

            return await Run(operations, cancellationToken);
        }

        private async Task<ConsoleScriptOperation> GetOperationFromRow(string row, CancellationToken cancellationToken = default)
        {
            var splittedRow = row.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var operationStringPart = splittedRow[0];
            var argumentValueStringPart = splittedRow[1];

            if (!stringOperationMapping.TryGetValue(operationStringPart, out var operation)) 
            {
                throw new ArgumentException($"Invalid operation in row '{row}'");
            }

            if (!int.TryParse(argumentValueStringPart, out var argument))
            {
                throw new ArgumentException($"Invalid argument in row '{row}'");
            }

            return new ConsoleScriptOperation()
            {
                Argument = argument,
                Type = operation
            };
        }
        public async Task<IEnumerable<ConsoleScriptOperation>> GetOperationsFromScript(string script, CancellationToken cancellationToken = default) 
        {
            var operations = new LinkedList<ConsoleScriptOperation>();    
            var rows = script.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            rows.ToList().ForEach( async row =>
            {
                operations.AddLast(await GetOperationFromRow(row, cancellationToken));
            });

            return operations;
        }
    }
}
