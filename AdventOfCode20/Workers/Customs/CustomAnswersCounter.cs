using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode20.Workers.Customs
{
    public class CustomAnswersCounter
    {
        public async Task<long> CountQuestionWithAgreement(string input, CancellationToken cancellationToken = default)
        {
            var groups = input.Split(new string[] { Environment.NewLine + Environment.NewLine },
                   StringSplitOptions.RemoveEmptyEntries);

            long total = 0;
            foreach (var group in groups)
            {
                var agreedQuestions = await ComputeGroupAgreedQuestions(group, cancellationToken);
                total += agreedQuestions.LongCount();
            }

            return total;
        }

        public async Task<long> CountUniqueAnswers(string input, CancellationToken cancellationToken = default)
        {
            var groups = input.Split(new string[] { Environment.NewLine + Environment.NewLine },
                               StringSplitOptions.RemoveEmptyEntries);

            long total = 0;
            foreach (var group in groups)
            {
                var groupAnswers = await ComupteGroupUniqueAnswers(group, cancellationToken);
                total += groupAnswers.LongCount();
            }

            return total;
        }

        private async Task<IEnumerable<char>> ComputeGroupAgreedQuestions(string group, CancellationToken cancellationToken)
        {
            HashSet<char> groupAgreedQuestions = null;
            var peopleAnswers = group.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            foreach (var personAnswers in peopleAnswers)
            {
                var personAnswersHash = personAnswers
                    .TrimEnd()
                    .ToCharArray()
                    .ToHashSet();

                if(groupAgreedQuestions == null)
                {
                    groupAgreedQuestions = personAnswersHash;
                }
                else
                {
                    groupAgreedQuestions.IntersectWith(personAnswers);
                }
            }

            return groupAgreedQuestions;
        }

        private async Task<IEnumerable<char>> ComupteGroupUniqueAnswers(string group, CancellationToken cancellationToken)
        {
            var groupUniqueAnswers = new HashSet<char>();
            var peopleAnswers = group.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            foreach(var personAnswers in peopleAnswers) 
            {
                var personAnswersHash =  personAnswers
                    .TrimEnd()
                    .ToCharArray()
                    .ToHashSet();

                groupUniqueAnswers.UnionWith(personAnswersHash);
            }

            return groupUniqueAnswers;
        }
    }
}
