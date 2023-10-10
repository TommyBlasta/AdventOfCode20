using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode20.InputParsers
{
    public interface IParser<TOutput>
    {
        public string InputTypeCode { get; }
        public Task<TOutput> Parse(string input, CancellationToken cancellationToken = default);
    }
}
