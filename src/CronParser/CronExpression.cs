using System;

namespace CronParser
{
    public class CronExpression
    {
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

        public CronValue Second { get; private set; }

        public CronValue Minute { get; private set; }

        public CronValue Hour { get; private set; }

        public CronValue DayOfMonth { get; private set; }

        public CronValue Month { get; private set; }

        public CronValue DayOfWeek { get; private set; }

        public CronValue Year { get; private set; }

        public DateTimeOffset? GetNextAvaliableTime(DateTimeOffset? afterTime = null)
        {
            var times = GetNextAvaliableTimes(afterTime, 1);
            if(times.Length == 0)
            {
                return null;
            }
            else
            {
                return times[0];
            }
        }

        public DateTimeOffset[] GetNextAvaliableTimes(DateTimeOffset? afterTime = null, int count = 1)
        {
            afterTime = afterTime ?? DateTimeOffset.UtcNow;
            ICronTimeBuilder builder = new CronTimeBuilder();
            builder.WithSecond(Second);
            builder.WithMinute(Minute);
            builder.WithHour(Hour);
            builder.WithDayOfMonth(DayOfMonth);
            builder.WithMonth(Month);
            builder.WithDayOfWeek(DayOfWeek);
            builder.WithYear(Year);
            return builder.GetNextTimes(afterTime.Value, count);
        }

        public DateTimeOffset[] GetAvaliableTimesBetween(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            CronTimeBuilder builder = new CronTimeBuilder();
            builder.WithSecond(Second);
            builder.WithMinute(Minute);
            builder.WithHour(Hour);
            builder.WithDayOfMonth(DayOfMonth);
            builder.WithMonth(Month);
            builder.WithDayOfWeek(DayOfWeek);
            builder.WithYear(Year);
            return builder.GetTimesBetween(startTime, endTime);
        }
    }
}
