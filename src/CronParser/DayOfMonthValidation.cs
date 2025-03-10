using System.Linq;

namespace CronParser
{
    public class DayOfMonthValidation
    {
        public static CronValue Validate(string cronValue)
        {
            if (cronValue == "*")
            {
                int[] values = Enumerable.Range(1, 31).ToArray();
                return new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if(cronValue == "l" || cronValue == "L")
            {
                return new CronValue() { Type = CronValueType.LastDayOfMonth };
            }
            else if (ValidationUtility.CollectionPattern.IsMatch(cronValue))
            {
                int[] values = ValidationUtility.ValidateCollection(cronValue, 31);
                return new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if (ValidationUtility.StepPattern.IsMatch(cronValue))
            {
                int[] values = ValidationUtility.ValidateStep(cronValue, 31);
                return new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if (ValidationUtility.RangePattern.IsMatch(cronValue))
            {
                int[] values = ValidationUtility.ValidateRange(cronValue, 31);
                return new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else
            {
                return null;
            }
        }
    }
}
