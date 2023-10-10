using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode20.Solvers
{
    public interface ISolver<TInput> where TInput : class
    {
        public string OperationType { get; }
        public Task<string> Solve(TInput input, CancellationToken cancellationToken = default);
    }
}
