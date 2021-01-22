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
        public int GetResult()
        {
            var nums = FindSumNumbers(GetInput(_inputStream));
            Console.WriteLine(nums);
            return nums.Item1 * nums.Item2;
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
        private Tuple<int, int> FindSumNumbers(int[] numbersInArray)
        {
            Array.Sort(numbersInArray);
            for (int i = 0; i < numbersInArray.Length; i++)
            {
                for (int a = i + 1; a < numbersInArray.Length; a++)
                {
                    if(numbersInArray[i] + numbersInArray[a] == _targetSum)
                    {
                        return new Tuple<int, int>(numbersInArray[i], numbersInArray[a]);
                    }    
                }
            }
            return null;
        }

    }
}
