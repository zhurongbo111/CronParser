using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CronParser
{
    public class CronTimeBuilder : ICronTimeBuilder
    {
        private CronValue _second;

        private CronValue _minute;

        private CronValue _hour;

        private CronValue _dayOfMonth;

        private CronValue _month;

        private CronValue _dayOfWeek;

        private CronValue _year;

        public ICronTimeBuilder WithSecond(CronValue second)
        {
            _second = second;
            return this;
        }

        public ICronTimeBuilder WithMinute(CronValue minute)
        {
            _minute = minute;
            return this;
        }

        public ICronTimeBuilder WithHour(CronValue hour)
        {
            _hour = hour;
            return this;
        }

        public ICronTimeBuilder WithDayOfMonth(CronValue dayOfMonth)
        {
            _dayOfMonth = dayOfMonth;
            return this;
        }

        public ICronTimeBuilder WithMonth(CronValue month)
        {
            _month = month;
            return this;
        }

        public ICronTimeBuilder WithDayOfWeek(CronValue dayOfWeek)
        {
            _dayOfWeek = dayOfWeek;
            return this;
        }

        public ICronTimeBuilder WithYear(CronValue year)
        {
            _year = year;
            return this;
        }

        public DateTimeOffset[] GetNextTimes(DateTimeOffset time, int count = 1)
        {
            return GetNextTimes(time, count, DateTimeOffset.MaxValue);
        }

        public DateTimeOffset[] GetTimesBetween(DateTimeOffset start, DateTimeOffset end)
        {
            return GetNextTimes(start, int.MaxValue, end);
        }

        private DateTimeOffset[] GetNextTimes(DateTimeOffset time, int count, DateTimeOffset endTime)
        {
            List<DateTimeOffset> result = new List<DateTimeOffset>();

            CronTime firsttime = GetNearestAvaliableDate(time);
            if ((firsttime == null))
            {
                return null;
            }
            else
            {
                DateTimeOffset currentTime = DateTimeOffset.MinValue;
                bool LoopNotEnd()
                {
                    return result.Count < count && currentTime < endTime;
                }

                int yearIndex = Array.IndexOf(_year.Values, firsttime.Year);
                int monthindex = Array.IndexOf(_month.Values, firsttime.Month);
                int dayOfMonthIndex = Array.IndexOf(
                    _dayOfMonth.Type == CronValueType.Collection ? _dayOfMonth.Values : new int[] { DateTime.DaysInMonth(firsttime.Year, firsttime.Month) },
                    firsttime.Day);
                int hourIndex = Array.IndexOf(_hour.Values, firsttime.Hour);
                int minuteIndex = Array.IndexOf(_minute.Values, firsttime.Minute);
                int secondIndex = Array.IndexOf(_second.Values, firsttime.Second);

                while (LoopNotEnd())// year loop
                {
                    int year = _year.Values[yearIndex];
                    while (LoopNotEnd())// month loop
                    {
                        int month = _month.Values[monthindex];
                        int[] days = _dayOfMonth.Type == CronValueType.Collection ? _dayOfMonth.Values : new int[] { DateTime.DaysInMonth(year, month) };
                        while (LoopNotEnd())// DayofMonth loop
                        {
                            int dayOfMonth = days[dayOfMonthIndex];
                            if(!CheckWeekDayLimit(year, month, dayOfMonth))
                            {
                                dayOfMonthIndex++;
                                // if day of month is not valid, then we need to reset the hour, minute and second
                                hourIndex = 0;
                                minuteIndex = 0;
                                secondIndex = 0;

                                if (dayOfMonthIndex == days.Length)
                                {
                                    dayOfMonthIndex = 0;
                                    break;
                                }

                                continue;
                            }
                            else
                            {
                                while (LoopNotEnd())// hour loop
                                {
                                    int hour = _hour.Values[hourIndex];
                                    while (LoopNotEnd())// minute loop
                                    {
                                        int minute = _minute.Values[minuteIndex];
                                        while (LoopNotEnd()) //second loop
                                        {
                                            int second = _second.Values[secondIndex];
                                            currentTime = new DateTimeOffset(year, month, dayOfMonth, hour, minute, second, time.Offset);
                                            result.Add(currentTime);
                                            secondIndex++;
                                            if (secondIndex == _second.Values.Length)
                                            {
                                                secondIndex = 0;
                                                break;
                                            }
                                        }

                                        minuteIndex++;
                                        if (minuteIndex == _minute.Values.Length)
                                        {
                                            minuteIndex = 0;
                                            break;
                                        }
                                    }

                                    hourIndex++;
                                    if (hourIndex == _hour.Values.Length)
                                    {
                                        hourIndex = 0;
                                        break;
                                    }
                                }

                                dayOfMonthIndex++;
                                if (dayOfMonthIndex == days.Length)
                                {
                                    dayOfMonthIndex = 0;
                                    break;
                                }
                            }
                        }

                        monthindex++;
                        if (monthindex >= _month.Values.Length)
                        {
                            monthindex = 0;
                            break;
                        }
                    }

                    yearIndex++;
                    if(yearIndex >= _year.Values.Length)
                    {
                        break;
                    }
                }
            }

            return result.Any() ? result.ToArray() : null;
        }

        private bool CheckWeekDayLimit(int year, int month, int dayOfMonth)
        {
            if(dayOfMonth > DateTime.DaysInMonth(year, month))
            {
                return false;
            }

            var date = new DateTimeOffset(year, month, dayOfMonth, 0, 0, 0, TimeSpan.Zero);
            switch (_dayOfWeek.Type)
            {
                case CronValueType.Collection:
                    return _dayOfWeek.Values.Contains((int)date.DayOfWeek);
                case CronValueType.LastWeekDay:
                    DateTimeOffset lastWeekDay = GetMatchedDate(year, month, _dayOfWeek.Values[0]);
                    return lastWeekDay.Day == dayOfMonth;
                case CronValueType.DayOfSeqencingWeek:
                    DateTimeOffset seqencingWeekDay = GetMatchedDate(year, month, _dayOfWeek.Values[0], _dayOfWeek.Values[1]);
                    return seqencingWeekDay != DateTimeOffset.MinValue && seqencingWeekDay.Day == dayOfMonth;
                default:
                    return false;
            };
        }

        private CronTime GetNearestAvaliableDate(DateTimeOffset time)
        {
            int secondIndex = FindNextAvaliableIndex(time.Second, _second.Values, false);
            bool secondReset = secondIndex == -1;
            int minuteIndex = FindNextAvaliableIndex(time.Minute, _minute.Values, !secondReset);
            bool minuteReset = minuteIndex == -1;
            int hourIndex = FindNextAvaliableIndex(time.Hour, _hour.Values, !minuteReset);
            bool hourReset = hourIndex == -1;
            int dayOfMonthIndex = FindNextAvaliableIndex(time.Day, 
                _dayOfMonth.Type == CronValueType.Collection ? _dayOfMonth.Values : new int[] { DateTime.DaysInMonth(time.Year,time.Month) }, !hourReset);
            bool dayOfMonthReset = dayOfMonthIndex == -1;
            int monthIndex = FindNextAvaliableIndex(time.Month, _month.Values, !dayOfMonthReset);
            bool monthReset = monthIndex == -1;
            int yearIndex = FindNextAvaliableIndex(time.Year, _year.Values, !monthReset);
            if(yearIndex == -1)
            {
                return null;
            }
            else
            {
                int year = _year.Values[yearIndex];
                int month = year != time.Year ? _month.Values[0] : _month.Values[monthIndex];
                int maxDay = DateTime.DaysInMonth(year, month);
                int[] days = _dayOfMonth.Type == CronValueType.Collection ? _dayOfMonth.Values : new int[] {maxDay };
                int dayOfMonth = year != time.Year || month != time.Month ? days[0] : days[dayOfMonthIndex];
                int hour = year != time.Year || month != time.Month || dayOfMonth != time.Day ? _hour.Values[0] : _hour.Values[hourIndex];
                int minute = year != time.Year || month != time.Month || dayOfMonth != time.Day || hour != time.Hour ? _minute.Values[0] : _minute.Values[minuteIndex];
                int second = year != time.Year || month != time.Month || dayOfMonth != time.Day || hour != time.Hour || minute != time.Minute ? _second.Values[0] : _second.Values[secondIndex];
                return new CronTime(year, month, dayOfMonth, hour, minute, second);
            }
        }

        private int FindNextAvaliableIndex(int target, int[] values, bool includeTargetIndex)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (i == 0 && target < values[i])
                {
                    return 0;
                }


                if (includeTargetIndex && target == values[i])
                {
                    return i;
                }

                if (i + 1 < values.Length && target >= values[i] && target < values[i + 1])
                {
                    return i + 1;
                }
            }

            return -1;
        }

        private DateTimeOffset GetMatchedDate(int year, int month, int WeekDay)
        {
            int days = DateTime.DaysInMonth(year, month);
            for (int i = days; i > 0; i--)
            {
                var date = new DateTimeOffset(year, month, i, 0, 0, 0, TimeSpan.Zero);
                if ((int)date.DayOfWeek == WeekDay)
                {
                    return date;
                }
            }

            return DateTimeOffset.MinValue;
        }

        private DateTimeOffset GetMatchedDate(int year, int month, int weekDay, int week)
        {
            var date = new DateTimeOffset(year, month, 1, 0, 0, 0, TimeSpan.Zero);
            int seqencingWeek = 0;
            for (int i = (int)date.DayOfWeek; date.Month == month;)
            {
                if(i == weekDay)
                {
                    seqencingWeek++;
                    if(seqencingWeek == week)
                    {
                        return date;
                    }  
                }

                i++;
                date = date.AddDays(1);
                if (i == 7)
                {
                    i = 0;
                }
            }

            return DateTimeOffset.MinValue;
        }
    }

    class CronTime
    {
        public CronTime(int year, int month, int day, int hour, int minute, int second)
        {
           Year = year; Month = month; Day = day; Hour = hour; Minute = minute; Second = second;
        }

        public  int Year { get; }

        public int Month { get; }

        public int Day { get; }

        public int Hour { get; }

        public int Minute { get; }

        public int Second { get; }

        public DateTimeOffset ToDateTimeOffSet(TimeSpan offset)
        {
            return new DateTimeOffset(Year, Month, Day, Hour, Minute, Second, offset);
        }
    }
}
