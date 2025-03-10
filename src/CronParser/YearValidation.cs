using System.Linq;
using System.Text.RegularExpressions;

namespace CronParser
{
    public class YearValidation
    {
        private const int Min = 1970;
        private const int Max = 2099;

        private static readonly Regex CollectionPattern = new Regex("^[0-9]{4}(,[0-9]{4})*$");
        private static readonly Regex RangePattern = new Regex("^[0-9]{4}-[0-9]{4}$");

        public static CronValue Validate(string cronValue)
        {
            if (cronValue == "*")
            {
                return new CronValue() { Values = Enumerable.Range(Min, Min - Max + 1).ToArray(), Type = CronValueType.Collection };
            }
            else if (CollectionPattern.IsMatch(cronValue))
            {
                int[] values = ValidationUtility.ValidateCollection(cronValue, Max);
                return new CronValue { Values = values, Type = CronValueType.Collection };
            }
            else if (RangePattern.IsMatch(cronValue))
            {
                int[] values = ValidationUtility.ValidateRange(cronValue, Max);
                return new CronValue { Values = values, Type = CronValueType.Collection };
            }
            else
            {
                return null;
            }
        }
    }
}
