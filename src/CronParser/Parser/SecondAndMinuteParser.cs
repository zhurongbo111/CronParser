using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CronParser.Parser
{
    public class SecondAndMinuteParser
    {
        public static CronValue Parser(string cronValue)
        {
            if(cronValue == "*")
            {
                return new CronValue() { Values = Enumerable.Range(0, 60).ToArray(), Type = CronValueType.Collection };
            }
            else if (ParserUtility.CollectionPattern.IsMatch(cronValue))
            {
                int[] values = ParserUtility.ValidateCollection(cronValue, 59, 0);
                return values == null ? null : new CronValue { Values = values, Type = CronValueType.Collection };
            }
            else if (ParserUtility.StepPattern.IsMatch(cronValue))
            {
                int[] values = ParserUtility.ValidateStep(cronValue, 59, 0);
                return values == null ? null : new CronValue { Values = values, Type = CronValueType.Collection };
            }
            else if (ParserUtility.RangePattern.IsMatch(cronValue))
            {
                int[] values = ParserUtility.ValidateRange(cronValue, 59, 0);
                return values == null ? null : new CronValue { Values = values, Type = CronValueType.Collection };
            }
            else
            {
                return null;
            }
        }
    }
}
