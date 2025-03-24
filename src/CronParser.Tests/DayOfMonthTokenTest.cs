using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronParser.Tests
{
    [TestClass]
    public sealed class DayOfMonthTokenTest
    {
        [TestMethod]
        [DataRow("0 0 0 5-7 * * *", new string[] { "2025-01-06 00:00:00", "2025-01-07 00:00:00", "2025-02-05 00:00:00", "2025-02-06 00:00:00" })]
        [DataRow("0 0 0 6-8 * * *", new string[] { "2025-01-06 00:00:00", "2025-01-07 00:00:00", "2025-01-08 00:00:00", "2025-02-06 00:00:00" })]
        [DataRow("0 0 0 4-7 * * *", new string[] { "2025-01-06 00:00:00", "2025-01-07 00:00:00", "2025-02-04 00:00:00", "2025-02-05 00:00:00" })]
        [DataRow("0 0 0 4-5 * * *", new string[] { "2025-02-04 00:00:00", "2025-02-05 00:00:00", "2025-03-04 00:00:00", "2025-03-05 00:00:00" })]
        [DataRow("0 0 0 2-4 * * *", new string[] { "2025-02-02 00:00:00", "2025-02-03 00:00:00", "2025-02-04 00:00:00", "2025-03-02 00:00:00" })]
        public void TestDayRange(string cron, string[] expectedDates)
        {
            DateTimeOffset time = new DateTimeOffset(2025, 1, 5, 0, 0, 0, TimeSpan.Zero);
            CronExpression cronExpression = CronExpressionParser.Parse(cron);
            DateTimeOffset[] actualDates = cronExpression.GetNextAvailableTimes(time, expectedDates.Length);
            Assert.AreEqual(expectedDates.Length, actualDates.Length);
            for (int i = 0; i < expectedDates.Length; i++)
            {
                Assert.AreEqual(expectedDates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        [TestMethod]
        [DataRow("0 0 0 5,6,7 * * *", new string[] { "2025-01-06 00:00:00", "2025-01-07 00:00:00", "2025-02-05 00:00:00", "2025-02-06 00:00:00" })]
        [DataRow("0 0 0 6,7,8 * * *", new string[] { "2025-01-06 00:00:00", "2025-01-07 00:00:00", "2025-01-08 00:00:00", "2025-02-06 00:00:00" })]
        [DataRow("0 0 0 4,5,6,7 * * *", new string[] { "2025-01-06 00:00:00", "2025-01-07 00:00:00", "2025-02-04 00:00:00", "2025-02-05 00:00:00" })]
        [DataRow("0 0 0 4,5 * * *", new string[] { "2025-02-04 00:00:00", "2025-02-05 00:00:00", "2025-03-04 00:00:00", "2025-03-05 00:00:00" })]
        [DataRow("0 0 0 2,3,4 * * *", new string[] { "2025-02-02 00:00:00", "2025-02-03 00:00:00", "2025-02-04 00:00:00", "2025-03-02 00:00:00" })]
        public void TestDayDelimiter(string cron, string[] expectedDates)
        {
            DateTimeOffset time = new DateTimeOffset(2025, 1, 5, 0, 0, 0, TimeSpan.Zero);
            CronExpression cronExpression = CronExpressionParser.Parse(cron);
            DateTimeOffset[] actualDates = cronExpression.GetNextAvailableTimes(time, expectedDates.Length);
            Assert.AreEqual(expectedDates.Length, actualDates.Length);
            for (int i = 0; i < expectedDates.Length; i++)
            {
                Assert.AreEqual(expectedDates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        [TestMethod]
        [DataRow("0 0 0 l * * *", new string[] { "2025-10-31 00:00:00", "2025-11-30 00:00:00", "2025-12-31 00:00:00", "2026-01-31 00:00:00" })]
        public void TestDayLast(string cron, string[] expectedDates)
        {
            foreach (var time in new DateTimeOffset[] { new DateTimeOffset(2025, 10, 1, 0, 0, 1, TimeSpan.Zero),
            new DateTimeOffset(2025, 10, 1, 0, 2, 1, TimeSpan.Zero),
            new DateTimeOffset(2025, 10, 1, 3, 2, 1, TimeSpan.Zero),
            new DateTimeOffset(2025, 10, 1, 0, 0, 0, TimeSpan.Zero)})
            {
                CronExpression cronExpression = CronExpressionParser.Parse(cron);
                DateTimeOffset[] actualDates = cronExpression.GetNextAvailableTimes(time, expectedDates.Length);
                Assert.AreEqual(expectedDates.Length, actualDates.Length);
                for (int i = 0; i < expectedDates.Length; i++)
                {
                    Assert.AreEqual(expectedDates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
        }
    }
}
