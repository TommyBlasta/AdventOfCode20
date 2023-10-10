using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode20.Workers.Console
{
    public class ScriptFixer
    {
        private readonly ConsoleScriptRunner _scriptRunner;

        public ScriptFixer() 
        {
            _scriptRunner = new ConsoleScriptRunner();
        }

        public async Task<(IEnumerable<ConsoleScriptOperation> FixedScript, RunResult ScriptResult)> FixScript(string script, CancellationToken cancellationToken = default)
        {
            var fixedScripts = await GetPossibleFixedScripts(script, cancellationToken);

            foreach (var fixedScript in fixedScripts) 
            {
                var runResult = await _scriptRunner.Run(fixedScript.ToArray(), cancellationToken);

                if (runResult.Finished)
                {
                    return (fixedScript, runResult);
                }
            }

            return (null, null);
        }

        private async Task<List<IEnumerable<ConsoleScriptOperation>>> GetPossibleFixedScripts(string script, CancellationToken cancellationToken = default)
        {
            var scriptOperations = new List<(ConsoleScriptOperation Operation, int Order)>();
            var readOperations = (await _scriptRunner.GetOperationsFromScript(script, cancellationToken)).ToArray();

            for(var i = 0; i < readOperations.Count() ;i++)
            {
                var operation = readOperations[i];
                scriptOperations.Add((operation, i));
            }

            var possibleChanges = scriptOperations
                .Where(x => x.Operation.Type == OperationType.Nop || x.Operation.Type == OperationType.Jmp)
                .Select(x => x.Order);

            var fixedScripts = new List<IEnumerable<ConsoleScriptOperation>>();
            foreach (var change in possibleChanges)
            {
                var fixedScript = await ProduceFixedScript(scriptOperations, change, cancellationToken);
                fixedScripts.Add(fixedScript);
            }

            return fixedScripts;
        }

        private async Task<IEnumerable<ConsoleScriptOperation>> ProduceFixedScript(IEnumerable<(ConsoleScriptOperation Operation, int Order)> operations,
            int change,
            CancellationToken cancellationToken)
        {
            var newScript = operations.Select(o =>
            {
                if(o.Order == change)
                {
                    return new ConsoleScriptOperation()
                    {
                        Argument = o.Operation.Argument,
                        Type = ReverseOperation(o.Operation.Type)
                    };
                }

                return new ConsoleScriptOperation()
                {
                    Argument = o.Operation.Argument,
                    Type = o.Operation.Type
                };
            });

            return newScript;
        }

        private OperationType ReverseOperation(OperationType operationType)
        {
            if(operationType == OperationType.Nop)
            {
                return OperationType.Jmp;
            }
            if(operationType == OperationType.Jmp)
            {
                return OperationType.Nop;
            }
            return operationType;
        }
    }
}
