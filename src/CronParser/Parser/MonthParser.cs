﻿using System.Collections.Generic;
using System.Linq;

namespace CronParser.Parser
{
    public class MonthParser
    {
        private static readonly Dictionary<string, string> MonthMap = new Dictionary<string, string>
        {
            { "JAN", "1" },
            { "FEB", "2" },
            { "MAR", "3" },
            { "APR", "4" },
            { "MAY", "5" },
            { "JUN", "6" },
            { "JUL", "7" },
            { "AUG", "8" },
            { "SEP", "9" },
            { "OCT", "10" },
            { "NOV", "11" },
            { "DEC", "12" },
        };

        public static CronValue Parser(string cronValue)
        {
            foreach (var month in MonthMap)
            {
                cronValue = cronValue.Replace(month.Key, month.Value);
            }

            if (cronValue == "*")
            {
                int[] values = Enumerable.Range(1, 12).ToArray();
                return values == null ? null : new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if (ParserUtility.CollectionPattern.IsMatch(cronValue))
            {
                int[] values = ParserUtility.ValidateCollection(cronValue, 12, 1);
                return values == null ? null : new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if (ParserUtility.StepPattern.IsMatch(cronValue))
            {
                int[] values = ParserUtility.ValidateStep(cronValue, 12, 1);
                return values == null ? null : new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if (ParserUtility.RangePattern.IsMatch(cronValue))
            {
                int[] values = ParserUtility.ValidateRange(cronValue, 12, 1);
                return values == null ? null : new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else
            {
                return null;
            }
        }
    }
}
