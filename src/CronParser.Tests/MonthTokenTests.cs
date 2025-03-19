using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronParser.Tests
{
    [TestClass]
    public sealed class MonthTokenTests
    {
        [TestMethod]
        [DataRow("0 0 0 1 5-7 * *", new string[] { "2025-06-01 00:00:00", "2025-07-01 00:00:00", "2026-05-01 00:00:00", "2026-06-01 00:00:00" })]
        [DataRow("0 0 0 1 6-8 * *", new string[] { "2025-06-01 00:00:00", "2025-07-01 00:00:00", "2025-08-01 00:00:00", "2026-06-01 00:00:00" })]
        [DataRow("0 0 0 1 4-7 * *", new string[] { "2025-06-01 00:00:00", "2025-07-01 00:00:00", "2026-04-01 00:00:00", "2026-05-01 00:00:00" })]
        [DataRow("0 0 0 1 4-5 * *", new string[] { "2026-04-01 00:00:00", "2026-05-01 00:00:00", "2027-04-01 00:00:00", "2027-05-01 00:00:00" })]
        [DataRow("0 0 0 1 2-4 * *", new string[] { "2026-02-01 00:00:00", "2026-03-01 00:00:00", "2026-04-01 00:00:00", "2027-02-01 00:00:00" })]
        public void TestDayRange(string cron, string[] expectedDates)
        {
            DateTimeOffset time = new DateTimeOffset(2025, 5, 1, 0, 0, 0, TimeSpan.Zero);
            CronExpression cronExpression = CronExpressionParser.Parse(cron);
            DateTimeOffset[] actualDates = cronExpression.GetNextAvaliableTimes(time, expectedDates.Length);
            Assert.AreEqual(expectedDates.Length, actualDates.Length);
            for (int i = 0; i < expectedDates.Length; i++)
            {
                Assert.AreEqual(expectedDates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        [TestMethod]
        [DataRow("0 0 0 1 5,6,7 * *", new string[] { "2025-06-01 00:00:00", "2025-07-01 00:00:00", "2026-05-01 00:00:00", "2026-06-01 00:00:00" })]
        [DataRow("0 0 0 1 6,7,8 * *", new string[] { "2025-06-01 00:00:00", "2025-07-01 00:00:00", "2025-08-01 00:00:00", "2026-06-01 00:00:00" })]
        [DataRow("0 0 0 1 4,5,6,7 * *", new string[] { "2025-06-01 00:00:00", "2025-07-01 00:00:00", "2026-04-01 00:00:00", "2026-05-01 00:00:00" })]
        [DataRow("0 0 0 1 4,5 * *", new string[] { "2026-04-01 00:00:00", "2026-05-01 00:00:00", "2027-04-01 00:00:00", "2027-05-01 00:00:00" })]
        [DataRow("0 0 0 1 2,3,4 * *", new string[] { "2026-02-01 00:00:00", "2026-03-01 00:00:00", "2026-04-01 00:00:00", "2027-02-01 00:00:00" })]
        public void TestDayDelimiter(string cron, string[] expectedDates)
        {
            DateTimeOffset time = new DateTimeOffset(2025, 5, 1, 0, 0, 0, TimeSpan.Zero);
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
