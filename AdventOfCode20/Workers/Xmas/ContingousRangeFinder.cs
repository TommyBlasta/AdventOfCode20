using AdventOfCode20.Workers.Xmas.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode20.Workers.Xmas
{
    public class ContingousRangeFinder
    {
        public Task<long> FindContingousRange(XmasInputDto input, long targetNumber, CancellationToken cancellationToken = default)
        {
            var dict = MarkNumbersAboveValue(input, targetNumber);
            var range = FindRange(dict, targetNumber) ?? throw new Exception("Couldn't find range in input");
            long result = GetResultFromRange(range, input);

            return Task.FromResult(result);
        }

        private long GetResultFromRange((int StartIndex, int EndIndex) range, XmasInputDto input)
        {
            var sequence = input.Sequence
                .ToList()
                .GetRange(range.StartIndex, (range.EndIndex - range.StartIndex) +1)
                .ToArray();

            var largest = sequence.Max();
            var lowest = sequence.Min();
            var sum = largest + lowest;
            return sum;
        }

        private (int StartIndex, int EndIndex)? FindRange(Dictionary<int, (bool IsAbove, long Number)> numbersDict, long targetNumber)
        {
            var endOfInputReached = false;
            var succeeded = false;
            var searchStartIndex = 0;

            while (!succeeded || endOfInputReached)
            {
                (bool Succeeded, int StartIndex, int EndIndex) range = TryFindFromStartPosition(searchStartIndex, numbersDict.ToArray(), targetNumber);

                if (range.Succeeded)
                {
                    return (range.StartIndex, range.EndIndex);
                }

                searchStartIndex = range.EndIndex;

                endOfInputReached = searchStartIndex >= numbersDict.Count() - 1;
            }

            return null;
        }

        private (bool Succeeded, int StartIndex, int EndIndex) TryFindFromStartPosition(int startIndex,
            KeyValuePair<int, (bool IsAbove, long Number)>[] numbers,
            long targetNumber)
        {
            var equals = false;
            var isAbove = numbers[startIndex].Value.IsAbove;
            var sum = numbers[startIndex].Value.Number;
            var shift = 0;

            while (!isAbove && sum != targetNumber)
            {
                shift++;
                var nextNum = numbers[startIndex + shift];
                sum += nextNum.Value.Number;
                isAbove = sum >= targetNumber;
                equals = sum == targetNumber;
            }

            if (isAbove && !equals)
            {
                return (false, startIndex, startIndex + 1);
            }

            return (true, startIndex, startIndex + shift);

        }

        private Dictionary<int, (bool IsAbove, long Number)> MarkNumbersAboveValue(XmasInputDto input, long targetNumber)
        {
            var numbersDict = new Dictionary<int, (bool IsAbove, long Number)>();
            var iterator = 0;

            foreach (var number in input.Sequence)
            {
                var isAbove = number >= targetNumber;
                numbersDict.Add(iterator, (isAbove, number));
                iterator++;
            }

            return numbersDict;
        }
    }
}
