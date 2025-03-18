using System;

namespace CronParser
{
    public interface ICronTimeBuilder
    {
        ICronTimeBuilder WithSecond(CronValue second);
        ICronTimeBuilder WithMinute(CronValue minute);
        ICronTimeBuilder WithHour(CronValue hour);
        ICronTimeBuilder WithDayOfMonth(CronValue dayOfMonth);
        ICronTimeBuilder WithMonth(CronValue month);
        ICronTimeBuilder WithDayOfWeek(CronValue dayOfWeek);
        ICronTimeBuilder WithYear(CronValue year);
        DateTimeOffset[] GetNextTimes(DateTimeOffset time, int count = 1);
        DateTimeOffset[] GetTimesBetween(DateTimeOffset start, DateTimeOffset end);
    }
}
