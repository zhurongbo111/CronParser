using System;

namespace CronParser
{
    public class CronExpressionParser
    {
        public static CronExpression Parse(string cron, bool includeYear = true)
        {
            cron = cron.Replace("?", "*");

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
                    if (includeYear)
                    {
                        secondToken = "*";
                        minuteToken = tokens[0];
                        hourToken = tokens[1];
                        dayOfMonthToken = tokens[2];
                        monthToken = tokens[3];
                        dayOfWeekToken = tokens[4];
                        yearToken = tokens[5];
                    }
                    else
                    {
                        secondToken = tokens[0];
                        minuteToken = tokens[1];
                        hourToken = tokens[2];
                        dayOfMonthToken = tokens[3];
                        monthToken = tokens[4];
                        dayOfWeekToken = tokens[5];
                        yearToken = "*";
                    }

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
                    throw new Exception();
            }

            CronValue second = SecondAndMinuteValidation.Validate(secondToken);
            CronValue minute = SecondAndMinuteValidation.Validate(minuteToken);
            CronValue hour = HourValidation.Validate(hourToken);
            CronValue dayOfMonth = DayOfMonthValidation.Validate(dayOfMonthToken);
            CronValue month = MonthValidation.Validate(monthToken);
            CronValue dayofWeek = DayOfWeekValidation.Validate(dayOfWeekToken);
            CronValue year = YearValidation.Validate(yearToken);

            return new CronExpression(year, minute, hour, dayOfMonth, month, dayofWeek, year);
        }
    }
}
