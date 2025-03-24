using CronParser.Parser;
using System;
using System.Collections.Generic;

namespace CronParser
{
    /// <summary>
    /// Provides methods to parse cron expressions.
    /// </summary>
    public static class CronExpressionParser
    {
        /// <summary>
        /// Parses a cron expression string and returns a CronExpression object.
        /// </summary>
        /// <param name="cron">The cron expression string.</param>
        /// <returns>A CronExpression object representing the parsed cron expression.</returns>
        public static CronExpression Parse(string cron)
        {
            return Parse(cron, true);
        }

        /// <summary>
        /// Tries to parse a cron expression string and returns a boolean indicating success or failure.
        /// </summary>
        /// <param name="cron">The cron expression string.</param>
        /// <param name="cronExpression">The resulting CronExpression object if parsing is successful, otherwise null.</param>
        /// <returns>True if parsing is successful, otherwise false.</returns>
        public static bool TryParse(string cron, out CronExpression cronExpression)
        {
            if (string.IsNullOrWhiteSpace(cron))
            {
                cronExpression = null;
                return false;
            }

            try
            {
                cronExpression = Parse(cron, false);
                return true;
            }
            catch
            {
                cronExpression = null;
                return false;
            }
        }

        /// <summary>
        /// Parses a cron expression string and returns a CronExpression object.
        /// </summary>
        /// <param name="cron">The cron expression string.</param>
        /// <param name="throwException">Indicates whether to throw an exception if parsing fails.</param>
        /// <returns>A CronExpression object representing the parsed cron expression, or null if parsing fails and throwException is false.</returns>
        private static CronExpression Parse(string cron, bool throwException)
        {
            if (string.IsNullOrWhiteSpace(cron))
                throw new ArgumentNullException(nameof(cron));

            cron = cron.Trim().Replace("?", "*").ToUpper();

            string[] tokens = cron.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string secondToken, minuteToken, hourToken, dayOfMonthToken, monthToken, dayOfWeekToken, yearToken;

            switch (tokens.Length)
            {
                case 5:
                    secondToken = "0";
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
                    if (throwException)
                    {
                        throw new CronFormatException("Cron must be 5 parts(minute to week), 6 parts(second to week) or 7 parts(second to year)");
                    }
                    else
                    {
                        return null;
                    }
            }

            var parsers = new List<(string, Func<string, CronValue>)>
                {
                    (secondToken, SecondAndMinuteParser.Parser),
                    (minuteToken, SecondAndMinuteParser.Parser),
                    (hourToken, HourParser.Parser),
                    (dayOfMonthToken, DayOfMonthParser.Parser),
                    (monthToken, MonthParser.Parser),
                    (dayOfWeekToken, DayOfWeekParser.Parser),
                    (yearToken, YearParser.Parser)
                };

            List<CronValue> cronValues = new List<CronValue>();

            foreach (var parser in parsers)
            {
                CronValue cronValue = parser.Item2(parser.Item1);
                if (cronValue == null)
                {
                    if (throwException)
                    {
                        throw new CronFormatException($"The expression {parser.Item1} is invalid");
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    cronValues.Add(cronValue);
                }
            }

            return new CronExpression(cronValues[0], cronValues[1], cronValues[2], cronValues[3], cronValues[4], cronValues[5], cronValues[6]);
        }
    }
}
