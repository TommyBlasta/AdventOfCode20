using AdventOfCode20.Workers;
using System;
using System.IO;

namespace AdventOfCode20
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputPath = Path.Combine(Directory.GetCurrentDirectory(), "inputs","input1.txt");
            var fileStream = File.OpenRead(inputPath);
            var sumFinder = new SumFinder(2020,fileStream);
            Console.WriteLine(sumFinder.GetResult());
            Console.ReadLine();
        }
    }
}
