using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CronParser.Parser
{
    public class HourParser
    {
        public static CronValue Parser(string cronValue)
        {
            if (cronValue == "*")
            {
                int[] values = Enumerable.Range(0, 24).ToArray();
                return values == null ? null : new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if (ParserUtility.CollectionPattern.IsMatch(cronValue))
            {
                int[] values = ParserUtility.ValidateCollection(cronValue, 23, 0);
                return values == null ? null : new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if (ParserUtility.StepPattern.IsMatch(cronValue))
            {
                int[] values = ParserUtility.ValidateStep(cronValue, 23, 0);
                return values == null ? null : new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if (ParserUtility.RangePattern.IsMatch(cronValue))
            {
                int[] values = ParserUtility.ValidateRange(cronValue, 23, 0);
                return values == null ? null : new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else
            {
                return null;
            }
        }
    }
}
