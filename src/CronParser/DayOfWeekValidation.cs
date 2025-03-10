using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CronParser
{
    public class DayOfWeekValidation
    {
        private static readonly Regex LastPattern = new Regex("^[0-6](l|L)$");
        private static readonly Regex Pattern = new Regex("^[0-6]#[1-5]$");

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

        public static CronValue Validate(string cronValue)
        {
            cronValue = cronValue.ToUpper();
            foreach (var weekDay in WeekDayMap)
            {
                cronValue = cronValue.Replace(weekDay.Key, weekDay.Value);
            }

            if (cronValue == "*")
            {
                int[] values = Enumerable.Range(0, 7).ToArray();
                return new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if (LastPattern.IsMatch(cronValue))
            {
                int weekDay = int.Parse(cronValue.Substring(0, cronValue.Length - 1));
                return new CronValue() { Values = new int[] { weekDay }, Type = CronValueType.DayOfLastWeek };

            }
            else if (Pattern.IsMatch(cronValue))
            {
                int[] values = cronValue.Split('#').Select(e => int.Parse(e)).ToArray();
                return new CronValue() { Values = values, Type = CronValueType.SeqencingDayOfWeek };
            }
            else if (ValidationUtility.CollectionPattern.IsMatch(cronValue))
            {
                int[] values = ValidationUtility.ValidateCollection(cronValue, 6);
                return new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if (ValidationUtility.StepPattern.IsMatch(cronValue))
            {
                int[] values = ValidationUtility.ValidateStep(cronValue, 6);
                return new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if (ValidationUtility.RangePattern.IsMatch(cronValue))
            {
                int[] values = ValidationUtility.ValidateRange(cronValue, 6);
                return new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else
            {
                return null;
            }
        }
    }
}
