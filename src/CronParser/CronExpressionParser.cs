using CronParser.Parser;
using System;

namespace CronParser
{
    public static class CronExpressionParser
    {
        public static CronExpression Parse(string cron)
        {
            if (string.IsNullOrWhiteSpace(cron))
                throw new ArgumentNullException(nameof(cron));

            cron = cron.Trim().Replace("?", "*").ToUpper();

            string[] tokens = cron.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string secondToken, minuteToken, hourToken, dayOfMonthToken, monthToken, dayOfWeekToken, yearToken;

            switch (tokens.Length)
            {
                case 5:
                    secondToken = "*";
                    minuteToken = tokens[0];
                    hourToken = tokens[1];
                    dayOfMonthToken = tokens[2];
                    monthToken = tokens[3];
                    dayOfWeekToken = tokens[4];
                    yearToken = "*";
                    break;
                case 6:
                    secondToken = tokens[0];
                    minuteToken = tokens[1];
                    hourToken = tokens[2];
                    dayOfMonthToken = tokens[3];
                    monthToken = tokens[4];
                    dayOfWeekToken = tokens[5];
                    yearToken = "*";
                    break;
                case 7:
                    secondToken = tokens[0];
                    minuteToken = tokens[1];
                    hourToken = tokens[2];
                    dayOfMonthToken = tokens[3];
                    monthToken = tokens[4];
                    dayOfWeekToken = tokens[5];
                    yearToken = tokens[6];
                    break;
                default:
                    throw new CronFormatException("Cron must be 5 parts(minute to week), 6 parts(second to week) or 7parts（second to year)");
            }

            CronValue second = SecondAndMinuteParser.Parser(secondToken) ?? throw new CronFormatException($"The expression {secondToken} is invalid");
            CronValue minute = SecondAndMinuteParser.Parser(minuteToken) ?? throw new CronFormatException($"The expression {minuteToken} is invalid");
            CronValue hour = HourParser.Parser(hourToken) ?? throw new CronFormatException($"The expression {hourToken} is invalid");
            CronValue dayOfMonth = DayOfMonthParser.Parser(dayOfMonthToken) ?? throw new CronFormatException($"The expression {dayOfMonthToken} is invalid");
            CronValue month = MonthParser.Parser(monthToken) ?? throw new CronFormatException($"The expression {monthToken} is invalid");
            CronValue dayofWeek = DayOfWeekParser.Parser(dayOfWeekToken) ?? throw new CronFormatException($"The expression {dayOfWeekToken} is invalid");
            CronValue year = YearParser.Parser(yearToken) ?? throw new CronFormatException($"The expression {yearToken} is invalid");

            return new CronExpression(second, minute, hour, dayOfMonth, month, dayofWeek, year);
        }
    }
}
