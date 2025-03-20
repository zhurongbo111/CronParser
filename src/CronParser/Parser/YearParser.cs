using System.Linq;
using System.Text.RegularExpressions;

namespace CronParser.Parser
{
    public class YearParser
    {
        private const int Min = 1970;
        private const int Max = 2099;

        private static readonly Regex CollectionPattern = new Regex("^[0-9]{4}(,[0-9]{4})*$");
        private static readonly Regex RangePattern = new Regex("^[0-9]{4}-[0-9]{4}$");
        private static readonly Regex StepPattern = new Regex(@"^(\*|[0-9]{4}(|-[0-9]{4}))/[0-9]{1,2}$");

        public static CronValue Parser(string cronValue)
        {
            if (cronValue == "*")
            {
                return new CronValue() { Values = Enumerable.Range(Min, Max - Min + 1).ToArray(), Type = CronValueType.Collection };
            }
            else if (CollectionPattern.IsMatch(cronValue))
            {
                int[] values = ParserUtility.ValidateCollection(cronValue, Max, Min);
                return values == null ? null : new CronValue { Values = values, Type = CronValueType.Collection };
            }
            else if (StepPattern.IsMatch(cronValue))
            {
                int[] values = ParserUtility.ValidateStep(cronValue, 2099, 1970);
                return values == null ? null : new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if (RangePattern.IsMatch(cronValue))
            {
                int[] values = ParserUtility.ValidateRange(cronValue, Max, Min);
                return values == null ? null : new CronValue { Values = values, Type = CronValueType.Collection };
            }
            else
            {
                return null;
            }
        }
    }
}
