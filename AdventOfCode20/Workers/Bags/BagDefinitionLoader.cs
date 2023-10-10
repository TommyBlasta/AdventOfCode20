using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode20.Workers.Bags
{
    public static class BagDefinitionLoader
    {
        public static IDictionary<string,IEnumerable<(int Number, string BagName)>> LoadDefintions(string input)
        {
            var dict = new ConcurrentDictionary<string, IEnumerable<(int Number, string BagName)>>();
            var lines = input.Split(new string[] { Environment.NewLine },
                   StringSplitOptions.RemoveEmptyEntries);

            foreach(var line in lines)
            {
                var bagName = GetBagName(line);
                var containedBags = GetContainedBags(line);
                dict.TryAdd(bagName, containedBags);
            }

            return dict;
        }

        private static string GetBagName(string line)
        {
            var bagNamePart = line.Split("contain", StringSplitOptions.RemoveEmptyEntries)[0];

            var cleanedBagName = bagNamePart.Replace(" bags ", "");

            return cleanedBagName;
        }

        private static IEnumerable<(int Number,string BagName)> GetContainedBags(string line)
        {
            var bagContentsPart = line.Split("contain", StringSplitOptions.RemoveEmptyEntries)[1];
            var bagContents = bagContentsPart.Split(", ", StringSplitOptions.RemoveEmptyEntries);

            var result = new List<(int Number, string BagName)>();    

            foreach(var bag in bagContents)
            {
                var bagParts = bag.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                //contain no other bags option
                if (bagParts.Length < 4)
                {
                    return new (int, string)[] {};
                }

                if(!int.TryParse(bagParts[0], out var number))
                {
                    throw new Exception("Couldn't parse number");
                }

                var bagName = string.Join(' ', bagParts[1], bagParts[2]);

                result.Add((number, bagName));
            }

            return result;
        }
    }
}
