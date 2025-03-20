using System.Linq;

namespace CronParser.Parser
{
    public class DayOfMonthParser
    {
        public static CronValue Parser(string cronValue)
        {
            if (cronValue == "*")
            {
                int[] values = Enumerable.Range(1, 31).ToArray();
                return new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if(cronValue == "L")
            {
                return new CronValue() { Type = CronValueType.LastDayOfMonth };
            }
            else if (ParserUtility.CollectionPattern.IsMatch(cronValue))
            {
                int[] values = ParserUtility.ValidateCollection(cronValue, 31, 1);
                return values == null ? null : new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else if (ParserUtility.RangePattern.IsMatch(cronValue))
            {
                int[] values = ParserUtility.ValidateRange(cronValue, 31, 1);
                return values == null ? null : new CronValue() { Values = values, Type = CronValueType.Collection };
            }
            else
            {
                return null;
            }
        }
    }
}
