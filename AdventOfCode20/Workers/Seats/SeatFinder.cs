using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode20.Workers.Seats
{
    public class SeatFinder
    {
        private readonly char[] GoLowerChars = new char[] { 'F', 'L' };
        private readonly char[] GoUpperChars = new char[] { 'B', 'R' };
        public async Task<SeatDefinition> FindSeat(string seatSpecification, int numberOfSeats, int numberOfColumns, CancellationToken cancallationToken = default)
        {
            var cleanedSeatSpec = CleanWhitespace(seatSpecification);
            var rowSpec = string.Join("", cleanedSeatSpec.Take(7));
            var columnSpec = string.Join("", cleanedSeatSpec.TakeLast(3));

            var row = await FindFinal(rowSpec, 128, cancellationToken: cancallationToken);
            var column = await FindFinal(columnSpec, 8, cancellationToken: cancallationToken);

            return new SeatDefinition()
            {
                Column = column.Value,
                Row = row.Value
            };

        }

        private async Task<int?> FindFinal(string inputSequence, int maximum, int minimum = 0, CancellationToken cancellationToken = default)
        {
            var interval = new Interval(minimum, maximum - 1);

            for (int i = 0; i < inputSequence.Length; i++)
            {
                interval = await GetNextInterval(inputSequence[i], interval);
                if(interval.HasResult)
                {
                    return interval.Lower;
                }
            }

            return interval.Lower;
        }

        private async Task<Interval> GetNextInterval(char switchChar, Interval currentInterval)
        {
            if(currentInterval.HasResult)
            {
                return currentInterval;
            }

            var goLower = GoLowerChars.Contains(switchChar);
            

            if (goLower)
            {
                var halfPoint = currentInterval.Lower + (currentInterval.IntervalLength / 2);
                return new Interval(currentInterval.Lower, halfPoint);
            }
            else
            {
                var halfPoint = (currentInterval.Lower + (currentInterval.IntervalLength / 2)) + 1;
                return new Interval(halfPoint, currentInterval.Upper);
            }
        }

        private string CleanWhitespace(string input)
        {
            return new string(input.Where(c => !char.IsWhiteSpace(c)).ToArray());
        }

        private class Interval
        {
            public Interval(int? lower, int? upper)
            {
                Lower = lower;
                Upper = upper;
            }

            public int? Lower { get; }
            public int? Upper { get; }
            public int? IntervalLength
            {
                get
                {
                    return Upper != null && Lower != null ? Upper - Lower : null;
                }
            }
            public bool HasResult => Lower != null && Upper != null && Upper.Value == Lower.Value;
        }
    }

}
