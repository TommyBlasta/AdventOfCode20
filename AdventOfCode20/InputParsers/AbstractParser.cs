using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode20.InputParsers
{
    public abstract class AbstractParser<TOutput> : IParser<TOutput>
    {
        public abstract string InputTypeCode { get; }
        protected abstract Task<TOutput> ParseInternal(string input, CancellationToken cancellationToken = default);

        public async Task<TOutput> Parse(string input, CancellationToken cancellationToken = default)
        {
            return await ParseInternal(input);
        }

        protected string CleanWhitespace(string input)
        {
            return new string(input.Where(c => !char.IsWhiteSpace(c)).ToArray());
        }

        protected IEnumerable<string> SplitOnNewLine(string input)
        {
            return input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
