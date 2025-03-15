using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CronParser
{
    internal class ParserUtility
    {
        internal static readonly Regex CollectionPattern = new Regex("^[0-9]{1,2}(,[0-9]{1,2})*$");
        internal static readonly Regex StepPattern = new Regex(@"^(\*|[0-9]{1,2})/[0-9]{1,2}$");
        internal static readonly Regex RangePattern = new Regex("^[0-9]{1,2}-[0-9]{1,2}$");

        internal static readonly int[] LastDayOfMonth = new int[1] { 32 };
        internal static readonly int[] LastDayOfWeek = new int[1] { 7 };

        public static int[] ValidateCollection(string cronValue, int max, int min)
        {
            int[] result = cronValue.Split(',').Select(e => int.Parse(e)).ToArray();
            if (result.Any(i => i > max || i < min))
            {
                return null;
            }

            return result;
        }

        public static int[] ValidateStep(string cronValue, int max, int min)
        {
            int[] values = cronValue.Split('/').Select(e => int.Parse(e)).ToArray();
            int start = values[0], step = values[1];
            List<int> result = new List<int>();
            for (int i = start; i <= max && i >= min; i += step)
            {
                result.Add(i);
            }

            return result.Any() ? result.ToArray() : null;
        }

        public static int[] ValidateRange(string cronValue, int max, int min)
        {
            int[] values = cronValue.Split('-').Select(e => int.Parse(e)).ToArray();
            if(values.Length != 2)
            {
                return null;
            }

            int start = values[0], end = values[1];
            if (start >= end || end > max || start < min)
            {
                return null;
            }

            return Enumerable.Range(start, end - start + 1).ToArray();
        }
    }
}
