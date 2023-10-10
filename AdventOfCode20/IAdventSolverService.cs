using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode20
{
    public interface IAdventSolverService
    {
        public Task<string> SolveByCode(AdventSolutionInformation adventSolutionInformation, string input, CancellationToken cancellationToken);

    }
}
