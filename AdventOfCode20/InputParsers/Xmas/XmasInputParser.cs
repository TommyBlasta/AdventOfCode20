using AdventOfCode20.Workers.Xmas.Dtos;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode20.InputParsers.Xmas
{
    public class XmasInputParser : AbstractParser<XmasInputDto>
    {
        public override string InputTypeCode => "xmas";

        protected override async Task<XmasInputDto> ParseInternal(string input, CancellationToken cancellationToken = default)
        {
            var rows = SplitOnNewLine(input);
            var numbers = rows.Select(x => long.Parse(CleanWhitespace(x)));
            return new XmasInputDto()
            {
                Sequence = numbers
            };
        }
    }
}
