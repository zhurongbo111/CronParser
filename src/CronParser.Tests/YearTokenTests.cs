using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronParser.Tests
{
    [TestClass]
    public sealed class YearTokenTests
    {
        [TestMethod]
        [DataRow("0 0 0 1 1 * 2023-2024", new string[] { })]
        [DataRow("0 0 0 1 1 * 2024-2025", new string[] { })]
        [DataRow("0 0 0 1 1 * 2024-2026", new string[] { "2026-01-01 00:00:00" })]
        [DataRow("0 0 0 1 1 * 2025-2026", new string[] { "2026-01-01 00:00:00" })]
        [DataRow("0 0 0 1 1 * 2026-2027", new string[] { "2026-01-01 00:00:00", "2027-01-01 00:00:00" })]
        public void TestYearRange(string cron, string[] expectedDates)
        {
            DateTimeOffset time = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);
            CronExpression cronExpression = CronExpressionParser.Parse(cron);
            if(expectedDates.Length == 0)
            {
                DateTimeOffset? actualDate = cronExpression.GetNextAvaliableTime(time);
                Assert.IsNull(actualDate);
                return;
            }
            else
            {
                DateTimeOffset[] actualDates = cronExpression.GetNextAvaliableTimes(time, expectedDates.Length);
                Assert.AreEqual(expectedDates.Length, actualDates.Length);
                for (int i = 0; i < expectedDates.Length; i++)
                {
                    Assert.AreEqual(expectedDates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
        }

        [TestMethod]
        [DataRow("0 0 0 1 1 * 2023,2024", new string[] { })]
        [DataRow("0 0 0 1 1 * 2024,2025", new string[] { })]
        [DataRow("0 0 0 1 1 * 2024,2025,2026", new string[] { "2026-01-01 00:00:00" })]
        [DataRow("0 0 0 1 1 * 2025,2026", new string[] { "2026-01-01 00:00:00" })]
        [DataRow("0 0 0 1 1 * 2026,2027", new string[] { "2026-01-01 00:00:00", "2027-01-01 00:00:00" })]
        public void TestYearDelimiter(string cron, string[] expectedDates)
        {
            DateTimeOffset time = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);
            CronExpression cronExpression = CronExpressionParser.Parse(cron);
            if (expectedDates.Length == 0)
            {
                DateTimeOffset? actualDate = cronExpression.GetNextAvaliableTime(time);
                Assert.IsNull(actualDate);
                return;
            }
            else
            {
                DateTimeOffset[] actualDates = cronExpression.GetNextAvaliableTimes(time, expectedDates.Length);
                Assert.AreEqual(expectedDates.Length, actualDates.Length);
                for (int i = 0; i < expectedDates.Length; i++)
                {
                    Assert.AreEqual(expectedDates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
        }
    }
}
