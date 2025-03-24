using System;

namespace CronParser
{
    /// <summary>
    /// Represents a parsed cron expression and provides methods to calculate the next available times.
    /// </summary>
    public class CronExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CronExpression"/> class.
        /// </summary>
        /// <param name="second">The second part of the cron expression.</param>
        /// <param name="minute">The minute part of the cron expression.</param>
        /// <param name="hour">The hour part of the cron expression.</param>
        /// <param name="dayOfMonth">The day of month part of the cron expression.</param>
        /// <param name="month">The month part of the cron expression.</param>
        /// <param name="dayOfWeek">The day of week part of the cron expression.</param>
        /// <param name="year">The year part of the cron expression.</param>
        public CronExpression(CronValue second, CronValue minute, CronValue hour,
            CronValue dayOfMonth, CronValue month, CronValue dayOfWeek, CronValue year)
        {
            Second = second;
            Minute = minute;
            Hour = hour;
            DayOfMonth = dayOfMonth;
            Month = month;
            DayOfWeek = dayOfWeek;
            Year = year;
        }

        internal CronValue Second { get; private set; }

        internal CronValue Minute { get; private set; }

        internal CronValue Hour { get; private set; }

        internal CronValue DayOfMonth { get; private set; }

        internal CronValue Month { get; private set; }

        internal CronValue DayOfWeek { get; private set; }

        internal CronValue Year { get; private set; }

        /// <summary>
        /// Gets the next available time after the specified time.
        /// </summary>
        /// <param name="afterTime">The time after which to find the next available time. If null, the current time is used.</param>
        /// <returns>The next available time, or null if no available time is found.</returns>
        public DateTimeOffset? GetNextAvailableTime(DateTimeOffset? afterTime = null)
        {
            var times = GetNextAvailableTimes(afterTime, 1);
            if (times == null || times.Length == 0)
            {
                return null;
            }
            else
            {
                return times[0];
            }
        }

        /// <summary>
        /// Gets the next available times after the specified time.
        /// </summary>
        /// <param name="afterTime">The time after which to find the next available times. If null, the current time is used.</param>
        /// <param name="count">The number of available times to return.</param>
        /// <returns>An array of the next available times.</returns>
        public DateTimeOffset[] GetNextAvailableTimes(DateTimeOffset? afterTime = null, int count = 1)
        {
            afterTime = afterTime ?? DateTimeOffset.UtcNow;
            ICronTimeBuilder builder = new CronTimeBuilder();
            // 链式调用构建器方法
            builder = builder.WithSecond(Second)
                             .WithMinute(Minute)
                             .WithHour(Hour)
                             .WithDayOfMonth(DayOfMonth)
                             .WithMonth(Month)
                             .WithDayOfWeek(DayOfWeek)
                             .WithYear(Year);
            return builder.GetNextTimes(afterTime.Value, count);
        }

        /// <summary>
        /// Gets the available times between the specified start and end times.
        /// </summary>
        /// <param name="startTime">The start time of the range.</param>
        /// <param name="endTime">The end time of the range.</param>
        /// <returns>An array of available times between the specified start and end times.</returns>
        public DateTimeOffset[] GetAvailableTimesBetween(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            ICronTimeBuilder builder = new CronTimeBuilder();
            // 链式调用构建器方法
            builder = builder.WithSecond(Second)
                             .WithMinute(Minute)
                             .WithHour(Hour)
                             .WithDayOfMonth(DayOfMonth)
                             .WithMonth(Month)
                             .WithDayOfWeek(DayOfWeek)
                             .WithYear(Year);
            return builder.GetTimesBetween(startTime, endTime);
        }
    }
}
