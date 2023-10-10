using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode20.Workers.PassportCheck
{
    public class HeighPassportCheck : PassportCheckAbstract
    {
        public override string Property => "hgt";

        protected override bool CheckProperty(string propertyValue)
        {
            var heightType = DetermineHeightType(propertyValue);
            var numberPartOfValue = string.Join("", propertyValue.ToCharArray()
                .ToList()
                .GetRange(0, propertyValue.Length - 2));

            if(!int.TryParse(numberPartOfValue, out var numberValue))
            {
                return false;   
            }

            if (heightType == HeightType.Invalid)
            {
                return false;
            }

            bool isInRange = false;

            if(heightType == HeightType.Cm)
            {
                isInRange = IsInRangeInclusive(numberValue, 150, 193);
            }

            if (heightType == HeightType.In)
            {
                isInRange = IsInRangeInclusive(numberValue, 59, 76);
            }

            return isInRange;
        }

        private HeightType DetermineHeightType(string value)
        {
            var lastTwoChars = string.Join("", value.TakeLast(2));

            if(lastTwoChars == "cm")
            {
                return HeightType.Cm;
            }

            if(lastTwoChars == "in")
            {
                return HeightType.In;
            }

            return HeightType.Invalid;
        }

        private enum HeightType
        {
            Cm,
            In,
            Invalid
        }
    }
}
