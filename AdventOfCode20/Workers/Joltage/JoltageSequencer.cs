using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode20.Workers.Joltage
{
    public class JoltageSequencer
    {
        public async Task<JoltageReport> ComputeJoltageDifferences(IEnumerable<int> adapterDefinitions, CancellationToken cancellationToken = default)
        {
            var builtInAdapter = adapterDefinitions.Max() + 3;
            var sorted = adapterDefinitions
                .Append(0)
                .Append(builtInAdapter)
                .OrderBy(x => x);
            return ComputeDifferences(sorted);
        }

        private JoltageReport ComputeDifferences(IOrderedEnumerable<int> sorted)
        {
            var result = new JoltageReport();
            int? previous = null;

            foreach(var adapterDefinition in sorted) 
            {
                if(previous == null)
                {
                    previous = adapterDefinition;
                }
                else
                {
                    var difference = adapterDefinition - previous;
                    result.Update(difference.Value);
                    previous = adapterDefinition;
                }
            }

            return result;
            
        }
    }
}
