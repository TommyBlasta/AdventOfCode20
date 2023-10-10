using AdventOfCode20.InputParsers.Joltage;
using AdventOfCode20.InputParsers.Xmas;
using AdventOfCode20.Workers;
using AdventOfCode20.Workers.Bags;
using AdventOfCode20.Workers.Console;
using AdventOfCode20.Workers.Customs;
using AdventOfCode20.Workers.Joltage;
using AdventOfCode20.Workers.Seats;
using AdventOfCode20.Workers.Xmas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode20
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var inputPath = Path.Combine(Directory.GetCurrentDirectory(), "inputs","input1.txt");
            var fileStream = File.OpenRead(inputPath);
            var sumFinder = new SumFinder(2020,fileStream);
            Console.WriteLine(sumFinder.GetResult());

            //4
            var inputString = ReadInputFile(Path.Combine(Directory.GetCurrentDirectory(), "inputs", "input4_1.txt"));
            var passPortChecker = new PassportChecker();

            var passPorts = PassportParser.GetSeparatePassports(inputString);
            var totalPassports = passPorts.Count();
            var validPassports = 0;


            foreach (var passPort in passPorts) 
            {
                var passportIsValid = await passPortChecker.CheckIsValid(new PassportInput()
                {
                    PassportRawText = passPort
                },
                new FieldConfig()
                {
                    RequiredFields = new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" }
                });

                if (passportIsValid)
                {
                    validPassports++;
                }
            }

            Console.WriteLine($"Total:{totalPassports} Valid:{validPassports}");

            //5
            inputString = ReadInputFile(Path.Combine(Directory.GetCurrentDirectory(), "inputs", "input5_1.txt"));
            var seatFinder = new SeatFinder();
            var seatSequences = inputString.Split('\n', options: StringSplitOptions.RemoveEmptyEntries);
            var seats = new List<SeatDefinition>();
            foreach(var seatSequence in seatSequences)
            {
                var seat = await seatFinder.FindSeat(seatSequence, 128, 8);
                seats.Add(seat);
            }

            var maxSeatId = seats.Max(x => x.Id);
            var orederedSeatIds = seats.Where(y => !seats.Any(x => x.Id == y.Id + 1));
            

            Console.WriteLine($"Max seat id:{maxSeatId}");
            Console.WriteLine($"My seat:{orederedSeatIds.Min(x => x.Id) + 1}");

            //6
            inputString = ReadInputFile(Path.Combine(Directory.GetCurrentDirectory(), "inputs", "input6_1.txt"));
            var customsChecker = new CustomAnswersCounter();
            var numberOfUniqueAnswers = await customsChecker.CountUniqueAnswers(inputString);
            var numberOfQuestionsInAgreement = await customsChecker.CountQuestionWithAgreement(inputString);

            Console.WriteLine($"The total unique answers are:{numberOfUniqueAnswers}");
            Console.WriteLine($"The total questions in agreement are:{numberOfQuestionsInAgreement}");

            //7
            inputString = ReadInputFile(Path.Combine(Directory.GetCurrentDirectory(), "inputs", "input7_1.txt"));
            var definitions = BagDefinitionLoader.LoadDefintions(inputString);

            var bagExpander = new BagExpander(definitions);

            var containedBags = new Dictionary<string, Dictionary<string,long>>();

            var bagCounter = 1;

            foreach(var bag in definitions.OrderBy(x => x.Value.Sum(x => x.Number)).Select(x => x.Key))
            {
                Console.WriteLine($"Expanding bag {bag}, it's {bagCounter} out of {definitions.Keys.Count()}");
                var contents = await bagExpander.ContainedBags(bag);

                Console.WriteLine($"Expanded bag {bag}");
                bagCounter++;

                containedBags.Add(bag, contents);
            }

            var neededBag = "shiny gold";

            var validBags = containedBags.Where(x => x.Value.ContainsKey(neededBag))
                .Select(x => x.Key);

            var bagToCountContentsFor = "shiny gold";

            var bagsInside = containedBags[bagToCountContentsFor];

            var count = bagsInside.Sum(x => x.Value);

            Console.WriteLine($"There are {validBags.Count()} which can contain the bag {neededBag}");
            Console.WriteLine($"There are {count} bags contained in {bagToCountContentsFor} bag");

            //8
            inputString = ReadInputFile(Path.Combine(Directory.GetCurrentDirectory(), "inputs", "input8_1.txt"));
            var consoleScriptRunner = new ConsoleScriptRunner();
            var runResult = await consoleScriptRunner.Run(inputString);

            var scriptFixer = new ScriptFixer();
            var fixedScript = await scriptFixer.FixScript(inputString);

            Console.WriteLine($"The run {(runResult.Finished ? "finished" : "inifinity looped")} and the last accumulator value was {runResult.AccumulatorValue}");


            //9
            inputString = ReadInputFile(Path.Combine(Directory.GetCurrentDirectory(), "inputs", "input9_1.txt"));
            var parser = new XmasInputParser();
            var input = await parser.Parse(inputString);
            var nonCompliantFinder = new NonCompliantFinder();
            var nonCompliant =  await nonCompliantFinder.FindNonCompliant(input);

            var rangeFinder = new ContingousRangeFinder();
            var sum = await rangeFinder.FindContingousRange(input, nonCompliant);

            //10
            inputString = ReadInputFile(Path.Combine(Directory.GetCurrentDirectory(), "inputs", "input10_1.txt"));
            var parserJoltage = new JoltageParser();
            var inputJoltage = await parserJoltage.Parse(inputString);
            var joltageSequencer = new JoltageSequencer();

            var joltageResult = await joltageSequencer.ComputeJoltageDifferences(inputJoltage);

            Console.ReadLine();
        }

        static string ReadInputFile(string filePath)
        {
            var inputPath = Path.Combine(filePath);
            return File.ReadAllText(filePath);
        }
    }
}
