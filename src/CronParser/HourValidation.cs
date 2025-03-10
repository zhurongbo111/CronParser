using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CronParser
{
    public class HourValidation
    {
        public static CronValue Validate(string cronValue)
        {
            if (cronValue == "*")
            {
                int[] values = Enumerable.Range(0, 24).ToArray();
                return new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if (ValidationUtility.CollectionPattern.IsMatch(cronValue))
            {
                int[] values = ValidationUtility.ValidateCollection(cronValue, 23);
                return new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if (ValidationUtility.StepPattern.IsMatch(cronValue))
            {
                int[] values = ValidationUtility.ValidateStep(cronValue, 23);
                return new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if (ValidationUtility.RangePattern.IsMatch(cronValue))
            {
                int[] values = ValidationUtility.ValidateRange(cronValue, 23);
                return new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else
            {
                return null;
            }
        }
    }
}
