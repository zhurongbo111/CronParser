using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronParser.Tests
{
    [TestClass]
    public sealed class HourTokenTest
    {
        [TestMethod]
        [DataRow("0 0 5-7 * * * *", new string[] { "2025-01-01 06:00:00", "2025-01-01 07:00:00", "2025-01-02 05:00:00", "2025-01-02 06:00:00" })]
        [DataRow("0 0 6-8 * * * *", new string[] { "2025-01-01 06:00:00", "2025-01-01 07:00:00", "2025-01-01 08:00:00", "2025-01-02 06:00:00" })]
        [DataRow("0 0 4-7 * * * *", new string[] { "2025-01-01 06:00:00", "2025-01-01 07:00:00", "2025-01-02 04:00:00", "2025-01-02 05:00:00" })]
        [DataRow("0 0 4-5 * * * *", new string[] { "2025-01-02 04:00:00", "2025-01-02 05:00:00", "2025-01-03 04:00:00", "2025-01-03 05:00:00" })]
        [DataRow("0 0 2-4 * * * *", new string[] { "2025-01-02 02:00:00", "2025-01-02 03:00:00", "2025-01-02 04:00:00", "2025-01-03 02:00:00" })]
        public void TestHourRange(string cron, string[] expectedDates)
        {
            DateTimeOffset time = new DateTimeOffset(2025, 1, 1, 5, 0, 0, TimeSpan.Zero);
            CronExpression cronExpression = CronExpressionParser.Parse(cron);
            DateTimeOffset[] actualDates = cronExpression.GetNextAvaliableTimes(time, expectedDates.Length);
            Assert.AreEqual(expectedDates.Length, actualDates.Length);
            for (int i = 0; i < expectedDates.Length; i++)
            {
                Assert.AreEqual(expectedDates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        [TestMethod]
        [DataRow("0 0 1,2 * * * *", new string[] { "2025-01-02 01:00:00", "2025-01-02 02:00:00", "2025-01-03 01:00:00", "2025-01-03 02:00:00" })]
        [DataRow("0 0 2,5 * * * *", new string[] { "2025-01-02 02:00:00", "2025-01-02 05:00:00", "2025-01-03 02:00:00", "2025-01-03 05:00:00" })]
        [DataRow("0 0 2,5,7 * * * *", new string[] { "2025-01-01 07:00:00", "2025-01-02 02:00:00", "2025-01-02 05:00:00", "2025-01-02 07:00:00" })]
        [DataRow("0 0 5,7 * * * *", new string[] { "2025-01-01 07:00:00", "2025-01-02 05:00:00", "2025-01-02 07:00:00", "2025-01-03 05:00:00" })]
        [DataRow("0 0 6,7 * * * *", new string[] { "2025-01-01 06:00:00", "2025-01-01 07:00:00", "2025-01-02 06:00:00", "2025-01-02 07:00:00" })]
        public void TestHourDelimiter(string cron, string[] expectedDates)
        {
            DateTimeOffset time = new DateTimeOffset(2025, 1, 1, 5, 0, 0, TimeSpan.Zero);
            CronExpression cronExpression = CronExpressionParser.Parse(cron);
            DateTimeOffset[] actualDates = cronExpression.GetNextAvaliableTimes(time, expectedDates.Length);
            Assert.AreEqual(expectedDates.Length, actualDates.Length);
            for (int i = 0; i < expectedDates.Length; i++)
            {
                Assert.AreEqual(expectedDates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        [TestMethod]
        [DataRow("0 0 2/8 * * * *", new string[] { "2025-01-01 10:00:00", "2025-01-01 18:00:00", "2025-01-02 02:00:00", "2025-01-02 10:00:00" })]
        [DataRow("0 0 5/7 * * * *", new string[] { "2025-01-01 12:00:00", "2025-01-01 19:00:00", "2025-01-02 05:00:00", "2025-01-02 12:00:00" })]
        [DataRow("0 0 6/10 * * * *", new string[] { "2025-01-01 06:00:00", "2025-01-01 16:00:00", "2025-01-02 06:00:00", "2025-01-02 16:00:00" })]
        public void TestHourStep(string cron, string[] expectedDates)
        {
            DateTimeOffset time = new DateTimeOffset(2025, 1, 1, 5, 0, 0, TimeSpan.Zero);
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
