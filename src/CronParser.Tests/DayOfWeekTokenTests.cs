using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronParser.Tests
{
    [TestClass]
    public sealed class DayOfWeekTokenTests
    {
        [TestMethod]
        [DataRow("0 0 0 * * 0-1 *", new string[] { "2025-01-12 00:00:00", "2025-01-13 00:00:00", "2025-01-19 00:00:00", "2025-01-20 00:00:00" })]
        [DataRow("0 0 0 * * 1-2 *", new string[] { "2025-01-13 00:00:00", "2025-01-14 00:00:00", "2025-01-20 00:00:00", "2025-01-21 00:00:00" })]
        [DataRow("0 0 0 * * 1-3 *", new string[] { "2025-01-08 00:00:00", "2025-01-13 00:00:00", "2025-01-14 00:00:00", "2025-01-15 00:00:00" })]
        [DataRow("0 0 0 * * 2-4 *", new string[] { "2025-01-08 00:00:00", "2025-01-09 00:00:00", "2025-01-14 00:00:00", "2025-01-15 00:00:00" })]
        [DataRow("0 0 0 * * 3-4 *", new string[] { "2025-01-08 00:00:00", "2025-01-09 00:00:00", "2025-01-15 00:00:00", "2025-01-16 00:00:00" })]
        public void TestDayOfWeekRange(string cron, string[] expectedDates)
        {
            DateTimeOffset time = new DateTimeOffset(2025, 1, 7, 0, 0, 0, TimeSpan.Zero);
            CronExpression cronExpression = CronExpressionParser.Parse(cron);
            DateTimeOffset[] actualDates = cronExpression.GetNextAvaliableTimes(time, expectedDates.Length);
            Assert.AreEqual(expectedDates.Length, actualDates.Length);
            for (int i = 0; i < expectedDates.Length; i++)
            {
                Assert.AreEqual(expectedDates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        [TestMethod]
        [DataRow("0 0 0 * * 0,1 *", new string[] { "2025-01-12 00:00:00", "2025-01-13 00:00:00", "2025-01-19 00:00:00", "2025-01-20 00:00:00" })]
        [DataRow("0 0 0 * * 1,2 *", new string[] { "2025-01-13 00:00:00", "2025-01-14 00:00:00", "2025-01-20 00:00:00", "2025-01-21 00:00:00" })]
        [DataRow("0 0 0 * * 1,2,3 *", new string[] { "2025-01-08 00:00:00", "2025-01-13 00:00:00", "2025-01-14 00:00:00", "2025-01-15 00:00:00" })]
        [DataRow("0 0 0 * * 2,3,4 *", new string[] { "2025-01-08 00:00:00", "2025-01-09 00:00:00", "2025-01-14 00:00:00", "2025-01-15 00:00:00" })]
        [DataRow("0 0 0 * * 3,4 *", new string[] { "2025-01-08 00:00:00", "2025-01-09 00:00:00", "2025-01-15 00:00:00", "2025-01-16 00:00:00" })]
        public void TestDayOfWeekDelimiter(string cron, string[] expectedDates)
        {
            DateTimeOffset time = new DateTimeOffset(2025, 1, 7, 0, 0, 0, TimeSpan.Zero);
            CronExpression cronExpression = CronExpressionParser.Parse(cron);
            DateTimeOffset[] actualDates = cronExpression.GetNextAvaliableTimes(time, expectedDates.Length);
            Assert.AreEqual(expectedDates.Length, actualDates.Length);
            for (int i = 0; i < expectedDates.Length; i++)
            {
                Assert.AreEqual(expectedDates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        [TestMethod]
        [DataRow("0 0 0 * * 3l *", new string[] { "2025-01-29 00:00:00", "2025-02-26 00:00:00", "2025-03-26 00:00:00", "2025-04-30 00:00:00" })]
        public void TestDayOfWeekLast(string cron, string[] expectedDates)
        {
            DateTimeOffset time = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);
            CronExpression cronExpression = CronExpressionParser.Parse(cron);
            DateTimeOffset[] actualDates = cronExpression.GetNextAvaliableTimes(time, expectedDates.Length);
            Assert.AreEqual(expectedDates.Length, actualDates.Length);
            for (int i = 0; i < expectedDates.Length; i++)
            {
                Assert.AreEqual(expectedDates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        [TestMethod]
        [DataRow("0 0 0 * * 5#3 *", new string[] { "2025-01-17 00:00:00", "2025-02-14 00:00:00", "2025-03-14 00:00:00", "2025-04-18 00:00:00" })]
        public void TestDayOfWeekSeqence(string cron, string[] expectedDates)
        {
            DateTimeOffset time = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);
            CronExpression cronExpression = CronExpressionParser.Parse(cron);
            DateTimeOffset[] actualDates = cronExpression.GetNextAvaliableTimes(time, expectedDates.Length);
            Assert.AreEqual(expectedDates.Length, actualDates.Length);
            for (int i = 0; i < expectedDates.Length; i++)
            {
                Assert.AreEqual(expectedDates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }
    }
}
