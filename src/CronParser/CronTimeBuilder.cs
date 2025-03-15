using System;
using System.Collections.Generic;
using System.Text;

namespace CronParser
{
    public class CronTimeBuilder
    {
        private CronValue _second;

        private CronValue _minute;

        private CronValue _hour;

        private CronValue _dayOfMonth;

        private CronValue _month;

        private CronValue _dayOfWeek;

        private CronValue _year;

        public void WithSecond(CronValue second)
        {
            _second = second;
        }

        public void WithMinute(CronValue minute)
        {
            _minute = minute;
        }

        public void WithHour(CronValue hour)
        {
            _hour = hour;
        }

        public void WithDayOfMonth(CronValue dayOfMonth)
        {
            _dayOfMonth = dayOfMonth;
        }

        public void WithMonth(CronValue month)
        {
            _month = month;
        }

        public void WithDayOfWeek(CronValue dayOfWeek)
        {
            _dayOfWeek = dayOfWeek;
        }

        public void WithYear(CronValue year)
        {
            _year = year;
        }

        public DateTimeOffset? GetNextTimes(DateTimeOffset time)
        {
            Tuple<bool, int> secondTuple = FindNearestValueInArray(time.Second, _second.Values);
            Tuple<bool, int> minuteTuple = FindNearestValueInArray(time.Minute, _minute.Values);
            Tuple<bool, int> hourTuple = FindNearestValueInArray(time.Hour, _hour.Values);
            Tuple<bool, int> dayOfMonthTuple = FindNearestValueInArray(time.Day, _dayOfMonth.Values);
            Tuple<bool, int> month = FindNearestValueInArray(time.Month, _month.Values);
            Tuple<bool, int> yearTuple = FindNearestValueInArray(time.Year, _year.Values);
            

            int secondStartIndex, minuteStartIndex, hourStartIndex, dayofMonthStartIndex, monthStartIndex, dayOfWeekStartIndex, yearStartIndex;
            secondStartIndex = secondTuple.Item2;


            return null;
        }

        private Tuple<bool, int> FindNearestValueInArray(int target, int[] values)
        {
            int targetIndex = Array.IndexOf(values, target);
            if( targetIndex > -1)
            {
                return targetIndex == values.Length - 1 ? 
                    new Tuple<bool, int>(true, 0) : 
                    new Tuple<bool, int>(false, targetIndex + 1);
            }
            else
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if(i == 0 && target < values[i])
                    {
                        return new Tuple<bool, int>(false, i);
                    }

                    if(target >= values[i] && i < values.Length - 1  && target < values[i + 1])
                    {
                        return new Tuple<bool, int>(false, i + 1);
                    }

                }

                return new Tuple<bool, int>(true, 0);
            }
        }
    }
}
