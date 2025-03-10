using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CronParser
{
    public class SecondAndMinuteValidation
    {
        public static CronValue Validate(string cronValue)
        {
            if(cronValue == "*")
            {
                return new CronValue() { Values = Enumerable.Range(0, 60).ToArray(), Type = CronValueType.Collection };
            }
            else if (ValidationUtility.CollectionPattern.IsMatch(cronValue))
            {
                int[] values = ValidationUtility.ValidateCollection(cronValue, 59);
                return new CronValue { Values = values, Type = CronValueType.Collection };
            }
            else if (ValidationUtility.StepPattern.IsMatch(cronValue))
            {
                int[] values = ValidationUtility.ValidateStep(cronValue, 59);
                return new CronValue { Values = values, Type = CronValueType.Collection };
            }
            else if (ValidationUtility.RangePattern.IsMatch(cronValue))
            {
                int[] values = ValidationUtility.ValidateRange(cronValue, 59);
                return new CronValue { Values = values, Type = CronValueType.Collection };
            }
            else
            {
                return null;
            }
        }
    }
}
