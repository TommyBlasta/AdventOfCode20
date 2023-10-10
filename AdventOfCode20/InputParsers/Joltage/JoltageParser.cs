using AdventOfCode20.Workers.Xmas.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode20.InputParsers.Joltage
{
    public class JoltageParser : AbstractParser<IEnumerable<int>>
    {
        public override string InputTypeCode => "jlt";

        protected async override Task<IEnumerable<int>> ParseInternal(string input, CancellationToken cancellationToken = default)
        {
            var splitInput = SplitOnNewLine(input);
            return splitInput.Select(i => int.Parse(i));
        }
    }
}
