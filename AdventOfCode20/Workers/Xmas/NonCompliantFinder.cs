using AdventOfCode20.Workers.Xmas.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode20.Workers.Xmas
{
    public class NonCompliantFinder
    {
        public Task<long> FindNonCompliant(XmasInputDto input, int preambleSize = 25, CancellationToken cancellationToken = default)
        {
            var preambleNums = input.Sequence.Take(preambleSize);

            var table = new SumSectionTable(preambleNums);
            var followingSequence = input.Sequence.TakeLast(input.Sequence.Count() - preambleSize);

            foreach(var number in followingSequence) 
            {
                if(!table.ShiftTable(number))
                { 
                    return Task.FromResult(number);
                }
            }

            return Task.FromResult(0l);
        }

        private class SumSectionTable
        {
            public IEnumerable<long> Header => header;
            private LinkedList<long> header;
            private LinkedList<LinkedList<long>> rows;
            public SumSectionTable(IEnumerable<long> preambleNumbers)
            {
                header = new LinkedList<long>(preambleNumbers);
                var preambleNumbersArray = preambleNumbers.ToArray();
                var tableSize = preambleNumbers.Count();
                rows = new LinkedList<LinkedList<long>>();

                for (int i = 0; i != tableSize - 1; i++)
                {
                    var initiatedRow = InitiateRow(preambleNumbersArray[i], preambleNumbersArray.TakeLast(tableSize - (i + 1)));
                    rows.AddLast(new LinkedList<long>(initiatedRow));
                }


            }

            private LinkedList<long> InitiateRow(long sumator, IEnumerable<long> otherNumbers)
            {
                return new LinkedList<long>(otherNumbers.Select(x => x + sumator));
            }

            public bool ShiftTable(long nextNumber)
            {
                if (!ValidateNextNumber(nextNumber))
                {
                    return false;
                }


                header.RemoveFirst();
                header.AddLast(nextNumber);

                rows.RemoveFirst();

                var headerArray = header.ToArray();
                var rowsArray = rows.ToArray();



                for (int i = 0; i < header.Count() - 2; i++)
                {
                    var nextSum = nextNumber + headerArray[i];
                    rowsArray[i].AddLast(nextSum);
                }
                var lastArrayValue = nextNumber + headerArray[header.Count() - 2];
                rows.AddLast(new LinkedList<long>(new[] { lastArrayValue }));

                return true;
            }

            public bool ValidateNextNumber(long nextNumber)
            {
                var hasSumInTable = rows.Any(row => row.Any(x => x == nextNumber));
                return hasSumInTable;
            }
        }
    }
}
