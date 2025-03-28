﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CronParser
{
    public class CronValue
    {
        public CronValueType Type { get; set; }

        public int[] Values { get; set; }
    }

    public enum CronValueType
    {
        Collection = 0,
        LastWeekDay = 1,
        LastDayOfMonth = 2,
        DayOfSeqencingWeek = 3
    }
}
