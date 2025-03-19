using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronParser.Tests
{
    [TestClass]
    public sealed class SecondTokenTests
    {
        [TestMethod] //Start  time: 2025-01-01 00:00:10
        [DataRow("8-9 * * * * * *", new string[] { "2025-01-01 00:01:08", "2025-01-01 00:01:09", "2025-01-01 00:02:08", "2025-01-01 00:02:09" })]
        [DataRow("8-10 * * * * * *", new string[] { "2025-01-01 00:01:08", "2025-01-01 00:01:09", "2025-01-01 00:01:10", "2025-01-01 00:02:08" })]
        [DataRow("9-11 * * * * * *", new string[] { "2025-01-01 00:00:11", "2025-01-01 00:01:09", "2025-01-01 00:01:10", "2025-01-01 00:01:11" })]
        [DataRow("10-13 * * * * * *", new string[] { "2025-01-01 00:00:11", "2025-01-01 00:00:12", "2025-01-01 00:00:13", "2025-01-01 00:01:10" })]
        [DataRow("12-14 * * * * * *", new string[] { "2025-01-01 00:00:12", "2025-01-01 00:00:13", "2025-01-01 00:00:14", "2025-01-01 00:01:12" })]
        public void TestSecondRange(string cron, string[] expectedDates)
        {
            CronExpression cronExpression = CronExpressionParser.Parse(cron);
            DateTimeOffset[] actualDates = cronExpression.GetNextAvaliableTimes(new DateTimeOffset(2025, 1, 1, 0, 0, 10, TimeSpan.Zero), expectedDates.Length);
            Assert.AreEqual(expectedDates.Length, actualDates.Length);
            for (int i = 0; i < expectedDates.Length; i++)
            {
                Assert.AreEqual(expectedDates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        [TestMethod] //Start  time: 2025-01-01 00:00:10
        [DataRow("2,4 * * * * * *", new string[] { "2025-01-01 00:01:02", "2025-01-01 00:01:04", "2025-01-01 00:02:02", "2025-01-01 00:02:04" })]
        [DataRow("2,10 * * * * * *", new string[] { "2025-01-01 00:01:02", "2025-01-01 00:01:10", "2025-01-01 00:02:02", "2025-01-01 00:02:10" })]
        [DataRow("2,10,14 * * * * * *", new string[] { "2025-01-01 00:00:14", "2025-01-01 00:01:02", "2025-01-01 00:01:10", "2025-01-01 00:01:14" })]
        [DataRow("10,14 * * * * * *", new string[] { "2025-01-01 00:00:14", "2025-01-01 00:01:10", "2025-01-01 00:01:14", "2025-01-01 00:02:10" })]
        [DataRow("12,14 * * * * * *", new string[] { "2025-01-01 00:00:12", "2025-01-01 00:00:14", "2025-01-01 00:01:12", "2025-01-01 00:01:14" })]
        public void TestSecondDelimiter(string cron, string[] expectedDates)
        {
            CronExpression cronExpression = CronExpressionParser.Parse(cron);
            DateTimeOffset[] actualDates = cronExpression.GetNextAvaliableTimes(new DateTimeOffset(2025, 1, 1, 0, 0, 10, TimeSpan.Zero), expectedDates.Length);
            Assert.AreEqual(expectedDates.Length, actualDates.Length);
            for (int i = 0; i < expectedDates.Length; i++)
            {
                Assert.AreEqual(expectedDates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        [TestMethod] //Start  time: 2025-01-01 00:00:10
        [DataRow("2/15 * * * * * *", new string[] { "2025-01-01 00:00:17", "2025-01-01 00:00:32", "2025-01-01 00:00:47", "2025-01-01 00:01:02" })]
        [DataRow("10/20 * * * * * *", new string[] { "2025-01-01 00:00:30", "2025-01-01 00:00:50", "2025-01-01 00:01:10", "2025-01-01 00:01:30" })]
        [DataRow("11/26 * * * * * *", new string[] { "2025-01-01 00:00:11", "2025-01-01 00:00:37", "2025-01-01 00:01:11", "2025-01-01 00:01:37" })]
        public void TestSecondStep(string cron, string[] expectedDates)
        {
            CronExpression cronExpression = CronExpressionParser.Parse(cron);
            DateTimeOffset[] actualDates = cronExpression.GetNextAvaliableTimes(new DateTimeOffset(2025, 1, 1, 0, 0, 10, TimeSpan.Zero), expectedDates.Length);
            Assert.AreEqual(expectedDates.Length, actualDates.Length);
            for (int i = 0; i < expectedDates.Length; i++)
            {
                Assert.AreEqual(expectedDates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }
    }
}
