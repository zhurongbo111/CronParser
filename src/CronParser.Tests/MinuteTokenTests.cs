using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronParser.Tests
{
    [TestClass]
    public sealed class MinuteTokenTests
    {
        [TestMethod] //Start  time: 2025-01-01 00:10:00
        [DataRow("0 2-4 * * * * *", new string[] { "2025-01-01 01:02:00", "2025-01-01 01:03:00", "2025-01-01 01:04:00", "2025-01-01 02:02:00" })]
        [DataRow("0 9-10 * * * * *", new string[] { "2025-01-01 01:09:00", "2025-01-01 01:10:00", "2025-01-01 02:09:00", "2025-01-01 02:10:00" })]
        [DataRow("0 9-11 * * * * *", new string[] { "2025-01-01 00:11:00", "2025-01-01 01:09:00", "2025-01-01 01:10:00", "2025-01-01 01:11:00" })]
        [DataRow("0 10-13 * * * * *", new string[] { "2025-01-01 00:11:00", "2025-01-01 00:12:00", "2025-01-01 00:13:00", "2025-01-01 01:10:00" })]
        [DataRow("0 12-14 * * * * *", new string[] { "2025-01-01 00:12:00", "2025-01-01 00:13:00", "2025-01-01 00:14:00", "2025-01-01 01:12:00" })]
        public void TestMinuteRange(string cron, string[] expectedDates)
        {
            DateTimeOffset time = new DateTimeOffset(2025, 1, 1, 0, 10, 0, TimeSpan.Zero);
            CronExpression cronExpression = CronExpressionParser.Parse(cron);
            DateTimeOffset[] actualDates = cronExpression.GetNextAvaliableTimes(time, expectedDates.Length);
            Assert.AreEqual(expectedDates.Length, actualDates.Length);
            for (int i = 0; i < expectedDates.Length; i++)
            {
                Assert.AreEqual(expectedDates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        [TestMethod] //Start  time: 2025-01-01 00:10:00
        [DataRow("0 2,4 * * * * *", new string[] { "2025-01-01 01:02:00", "2025-01-01 01:04:00", "2025-01-01 02:02:00", "2025-01-01 02:04:00" })]
        [DataRow("0 2,10 * * * * *", new string[] { "2025-01-01 01:02:00", "2025-01-01 01:10:00", "2025-01-01 02:02:00", "2025-01-01 02:10:00" })]
        [DataRow("0 2,10,14 * * * * *", new string[] { "2025-01-01 00:14:00", "2025-01-01 01:02:00", "2025-01-01 01:10:00", "2025-01-01 01:14:00" })]
        [DataRow("0 10,14 * * * * *", new string[] { "2025-01-01 00:14:00", "2025-01-01 01:10:00", "2025-01-01 01:14:00", "2025-01-01 02:10:00" })]
        [DataRow("0 12,14 * * * * *", new string[] { "2025-01-01 00:12:00", "2025-01-01 00:14:00", "2025-01-01 01:12:00", "2025-01-01 01:14:00" })]
        public void TestMinuteDelimiter(string cron, string[] expectedDates)
        {
            DateTimeOffset time = new DateTimeOffset(2025, 1, 1, 0, 10, 0, TimeSpan.Zero);
            CronExpression cronExpression = CronExpressionParser.Parse(cron);
            DateTimeOffset[] actualDates = cronExpression.GetNextAvaliableTimes(time, expectedDates.Length);
            Assert.AreEqual(expectedDates.Length, actualDates.Length);
            for (int i = 0; i < expectedDates.Length; i++)
            {
                Assert.AreEqual(expectedDates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        [TestMethod] //Start  time: 2025-01-01 00:10:00
        [DataRow("0 2/15 * * * * *", new string[] { "2025-01-01 00:17:00", "2025-01-01 00:32:00", "2025-01-01 00:47:00", "2025-01-01 01:02:00" })]
        [DataRow("0 10/20 * * * * *", new string[] { "2025-01-01 00:30:00", "2025-01-01 00:50:00", "2025-01-01 01:10:00", "2025-01-01 01:30:00" })]
        [DataRow("0 11/26 * * * * *", new string[] { "2025-01-01 00:11:00", "2025-01-01 00:37:00", "2025-01-01 01:11:00", "2025-01-01 01:37:00" })]
        public void TestMinuteStep(string cron, string[] expectedDates)
        {
            DateTimeOffset time = new DateTimeOffset(2025, 1, 1, 0, 10, 0, TimeSpan.Zero);
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
