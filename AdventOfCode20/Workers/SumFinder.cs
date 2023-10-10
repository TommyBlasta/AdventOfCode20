using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode20.Workers
{
    public class SumFinder
    {
        private int _targetSum;
        private Stream _inputStream;
        public SumFinder(int targetSum, Stream inputStream)
        {
            _targetSum = targetSum;
            _inputStream = inputStream;

        }
        public long GetResult()
        {
            var nums = FindSumNumbers(GetInput(_inputStream));
            //Console.WriteLine(String.Concat(nums.Select(x => x.ToString() + " ")));
            long result = 1;
            foreach(var num in nums)
            {
                result *= num;
            }
            return result;
        }
        private int[] GetInput(Stream dataStream)
        {
            var loadedInputNumbers = new LinkedList<int>();
            using StreamReader streamReader = new StreamReader(dataStream);
            while (!streamReader.EndOfStream)
            {
                int.TryParse(streamReader.ReadLine(), out var number);
                loadedInputNumbers.AddLast(number);
            }
            return loadedInputNumbers.ToArray();
        }
        private int[] FindSumNumbers(int[] numbersInArray)
        {
            Array.Sort(numbersInArray);
            for (int i = 0; i < numbersInArray.Length; i++)
            {
                for (int a = i + 1; a < numbersInArray.Length; a++)
                {
                    for (int b = a + 1; b < numbersInArray.Length; b++)
                    {
                        if (numbersInArray[i] + numbersInArray[a] + numbersInArray[b] == _targetSum)
                        {
                            return new int[] { numbersInArray[i], numbersInArray[a], numbersInArray[b] };
                        }

                    }

                }
            }
            return null;
        }

    }
}
