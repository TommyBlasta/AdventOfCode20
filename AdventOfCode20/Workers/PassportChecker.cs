using AdventOfCode20.Workers.PassportCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode20.Workers
{
    public class PassportChecker
    {
        private readonly char[] PassportPropertiesStringSeparators = new char[] { '\n', ' ' };
        private readonly Dictionary<string, PassportCheckAbstract> _checks;
        private char PassportPropertyKeyValueSeparator = ':';

        public PassportChecker()
        {
            var checks = new List<PassportCheckAbstract>()
            {
                new BirthYearPassportCheck(),
                new ExpirationYearPassportCheck(),
                new EyeColorPassportCheck(),
                new HairColorPassportCheck(),
                new HeighPassportCheck(),
                new IssueYearPassportCheck(),
                new PassportIdCheck()
            };

            _checks = checks.ToDictionary(x => x.Property, x => x);
        }

        public async Task<bool> CheckIsValid(PassportInput passportInput, FieldConfig fieldConfig, CancellationToken cancellationToken = default)
        {
            var passportProperties = GetPassportProperties(passportInput);

            var allRequiredFieldsPresent = fieldConfig.RequiredFields.All(f => passportProperties.ContainsKey(f));

            var checksResults = ValidatePropertyValues(passportProperties);
            
            return allRequiredFieldsPresent && checksResults.All(r => r.Valid);
        }

        private IEnumerable<(string Property, bool Valid)> ValidatePropertyValues(Dictionary<string, string> passportProperties)
        {
            var results = new List<(string Property, bool Valid)>();
            foreach(var passportProperty in passportProperties) 
            {
                if(_checks.TryGetValue(passportProperty.Key, out var check)) 
                {
                    var checkResult = check.Check(passportProperty.Value);
                    results.Add((passportProperty.Key, checkResult));
                }
            }

            return results;
        }

        private Dictionary<string,string> GetPassportProperties(PassportInput passportInput) 
        {
            var splitPropertiesStrings =  passportInput.PassportRawText.Split(PassportPropertiesStringSeparators);
            var passportPropertiesDict = splitPropertiesStrings
                .Select(GetPropertyAsKeyValue)
                .ToDictionary(x => x.Key, x => x.Value);

            return passportPropertiesDict;
        }

        private (string Key, string Value) GetPropertyAsKeyValue(string propertyString)
        {
            var keyValue = propertyString.Split(PassportPropertyKeyValueSeparator);
            return (keyValue[0], CleanWhitespace(keyValue[1]));
        }

        private string CleanWhitespace(string input)
        {
            return new string(input.Where(c => !char.IsWhiteSpace(c)).ToArray());
        }
    }
}
