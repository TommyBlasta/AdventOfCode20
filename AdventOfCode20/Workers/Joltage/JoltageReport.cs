using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode20.Workers.Joltage
{
    public class JoltageReport
    {
        public int OneJoltDifferencesNumber => _joltageCountDictionary[1];
        public int TwoJoltDifferencesNumber => _joltageCountDictionary[2];
        public int ThreeJoltDifferencesNumber => _joltageCountDictionary[3];
        public int[] DifferencesPattern => _differenceSequence.ToArray();

        private readonly IDictionary<int, int> _joltageCountDictionary;
        private readonly LinkedList<int> _differenceSequence;


        public JoltageReport()
        {
            _joltageCountDictionary = new Dictionary<int, int>();
            _differenceSequence = new LinkedList<int>();

            Enumerable.Range(1, 3).ToList().ForEach(n => _joltageCountDictionary.Add(n, 0));
        }

        public void Update(int differenceToAdd)
        {
            _differenceSequence.AddLast(differenceToAdd);
            _joltageCountDictionary[differenceToAdd]++;
        }
    }


}