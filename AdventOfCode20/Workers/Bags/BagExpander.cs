using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode20.Workers.Bags
{
    public class BagExpander
    {
        private readonly IDictionary<string, IEnumerable<(int Number, string BagName)>> _bagDefinitions;
        private readonly ConcurrentDictionary<string, Dictionary<string, long>> _bagContents;

        public BagExpander(IDictionary<string, IEnumerable<(int Number, string BagName)>> bagDefinitions)
        {
            _bagDefinitions = bagDefinitions;
            _bagContents = new ConcurrentDictionary<string, Dictionary<string, long>>();
        }

        public async Task <Dictionary<string, long>> ContainedBags(string bag, CancellationToken cancellationToken = default)
        {
            if(_bagContents.TryGetValue(bag, out var alreadyExpandedBag))
            {
                return alreadyExpandedBag;
            }

            var totalContainedBags = new Dictionary<string, long>();

            var bagContents = ExpandBag(bag);

            if(bagContents != null && bagContents.Any())
            {
                var groupped = GroupBags(bagContents);

                AddOrUpdateToDict(groupped, totalContainedBags);

                var trackedTasks = new List<(string BagName, Task <Dictionary<string, long>>)>();

                foreach( var bagItem in groupped) 
                {
                    var task = ContainedBags(bagItem.Key, cancellationToken);
                    trackedTasks.Add((bagItem.Key, task));
                }

                await Task.WhenAll(trackedTasks.Select(x => x.Item2));

                foreach( var results in trackedTasks)
                {
                    var numberOfBags = groupped[results.BagName];

                    AddOrUpdateToDict(results.Item2.Result, totalContainedBags, numberOfBags);
                }
            }

            _bagContents.TryAdd(bag, totalContainedBags);

            return totalContainedBags;
        }

        private IEnumerable<string> ExpandBag(string bag)
        {
            if (_bagDefinitions.TryGetValue(bag, out var bagContents))
            {
                var toReturn = new List<string>();
                bagContents.ToList().ForEach(x =>
                {
                    for(int i = 1; i <= x.Number; i++)
                    {
                        toReturn.Add(x.BagName);
                    }
                });

                return toReturn;
            }
            else
            {
                throw new System.Exception($"Couldn't find bag definition for: {bag}");
            }
        }

        private void AddOrUpdateToDict(string bagName, long number, IDictionary<string, long> destinationDict, long multiplier = 1)
        {
            if(!destinationDict.TryAdd(bagName, number * multiplier))
            {
                destinationDict[bagName] = destinationDict[bagName] + (number * multiplier);
            }
        }

        private void AddOrUpdateToDict(IDictionary<string, long> sourceDict, IDictionary<string, long> destinationDict, long multiplier = 1)
        {
            foreach(var sourceEntry in sourceDict)
            {
                AddOrUpdateToDict(sourceEntry.Key, sourceEntry.Value, destinationDict, multiplier);
            }
        }

        private IDictionary<string, long> GroupBags(IEnumerable<string> bags)
        {
            var groupped = bags.GroupBy(x => x)
                .Select(t => (BagName: t.Key, Count: t.LongCount()));

            var dict = groupped.ToDictionary(x => x.BagName, x => x.Count);

            return dict;
        }
    }
}
