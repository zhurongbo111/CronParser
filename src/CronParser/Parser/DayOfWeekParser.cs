using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CronParser.Parser
{
    public class DayOfWeekParser
    {
        private static readonly Regex LastPattern = new Regex("^[0-6](l|L)$");
        private static readonly Regex Pattern = new Regex("^[0-6]#[1-6]$");

        private static readonly Dictionary<string, string> WeekDayMap = new Dictionary<string, string>
        {
            { "SUN", "0" },
            { "MON", "1" },
            { "TUE", "2" },
            { "WED", "3" },
            { "THU", "4" },
            { "FRI", "5" },
            { "SAT", "6" }
        };

        public static CronValue Parser(string cronValue)
        {
            cronValue = cronValue.ToUpper();
            foreach (var weekDay in WeekDayMap)
            {
                cronValue = cronValue.Replace(weekDay.Key, weekDay.Value);
            }

            if (cronValue == "*")
            {
                int[] values = Enumerable.Range(0, 7).ToArray();
                return values == null ? null : new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if (LastPattern.IsMatch(cronValue))
            {
                int weekDay = int.Parse(cronValue.Substring(0, cronValue.Length - 1));
                if(weekDay < 0 || weekDay > 6)
                {
                    return null;
                }

                return new CronValue() { Values = new int[] { weekDay }, Type = CronValueType.LastWeekDay };

            }
            else if (Pattern.IsMatch(cronValue))
            {
                int[] values = cronValue.Split('#').Select(e => int.Parse(e)).ToArray();
                if(values.Length != 2 || (values[1]==5 && values[0] > 2))
                {
                    return null;
                }

                return new CronValue() { Values = values, Type = CronValueType.DayOfSeqencingWeek };
            }
            else if (ParserUtility.CollectionPattern.IsMatch(cronValue))
            {
                int[] values = ParserUtility.ValidateCollection(cronValue, 6, 0);
                return values == null ? null : new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if (ParserUtility.StepPattern.IsMatch(cronValue))
            {
                int[] values = ParserUtility.ValidateStep(cronValue, 6, 0);
                return values == null ? null : new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if (ParserUtility.RangePattern.IsMatch(cronValue))
            {
                int[] values = ParserUtility.ValidateRange(cronValue, 6, 0);
                return values == null ? null : new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else
            {
                return null;
            }
        }
    }
}
