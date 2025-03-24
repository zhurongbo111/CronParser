using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace CronParser.Tests
{
    [TestClass]
    public sealed class CronTests
    {
        [TestMethod]//Start  time: 2025-01-01 00:00:00
        [DataRow("* * * * * * *", "2025-01-01 00:00:01")]
        [DataRow("0 * * * * * *", "2025-01-01 00:01:00")]
        [DataRow("* 0 * * * * *", "2025-01-01 00:00:01")]
        [DataRow("* 1 * * * * *", "2025-01-01 00:01:00")]
        [DataRow("* * 0 * * * *", "2025-01-01 00:00:01")]
        [DataRow("* * 1 * * * *", "2025-01-01 01:00:00")]
        [DataRow("* * * 1 * * *", "2025-01-01 00:00:01")]
        [DataRow("* * * 2 * * *", "2025-01-02 00:00:00")]
        [DataRow("* * * * 1 * *", "2025-01-01 00:00:01")]
        [DataRow("* * * * 2 * *", "2025-02-01 00:00:00")]
        [DataRow("* * * * * 0 *", "2025-01-05 00:00:00")]
        [DataRow("* * * * * 3 *", "2025-01-01 00:00:01")]
        [DataRow("* * * * * * 2025", "2025-01-01 00:00:01")]
        [DataRow("* * * * * * 2026", "2026-01-01 00:00:00")]
        [DataRow("59 * * * * * *", "2025-01-01 00:00:59")]
        [DataRow("59 59 * * * * *", "2025-01-01 00:59:59")]
        [DataRow("59 59 23 * * * *", "2025-01-01 23:59:59")]
        [DataRow("59 59 23 31 * * *", "2025-01-31 23:59:59")]
        [DataRow("59 59 23 31 12 * *", "2025-12-31 23:59:59")]
        public void TestEveryToken(string cron, string expectedDate)
        {
            DateTimeOffset time = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var cronExpression = CronExpressionParser.Parse(cron);
            var nextTime = cronExpression.GetNextAvailableTime(time);

            Assert.IsTrue(nextTime.HasValue);
            Assert.AreEqual(expectedDate, nextTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        [TestMethod]
        [DataRow("* * * *")]
        [DataRow("* * * * * * * *")]
        [DataRow("60 * * * * * *")]
        [DataRow("* 60 * * * * *")]
        [DataRow("* * 24 * * * *")]
        [DataRow("* * * 32 * * *")]
        [DataRow("* * * * 13 * *")]
        [DataRow("* * * * * * 2100")]
        [DataRow("* * * * * 7 *")]
        [DataRow("0,60 * * * * * *")]
        [DataRow("1-60 * * * * * *")]
        [DataRow("60/2 * * * * * *")]
        [DataRow("* 1,60 * * * * *")]
        [DataRow("* 2-60 * * * * *")]
        [DataRow("* 60/5 * * * * *")]
        [DataRow("* * 1,24 * * * *")]
        [DataRow("* * 2-24 * * * *")]
        [DataRow("* * 24/6 * * * *")]
        [DataRow("* * * 1-32 * * *")]
        [DataRow("* * * 1,32 * * *")]
        [DataRow("* * * 32/2 * * *")]
        [DataRow("* * * * 1-13 * *")]
        [DataRow("* * * * 1,13 * *")]
        [DataRow("* * * * 13/3 * *")]
        [DataRow("* * * * * * 1971-2100")]
        [DataRow("* * * * * * 2025,2100")]
        [DataRow("* * * * * 1#7 *")]
        [DataRow("* * * * * 7l *")]
        [ExpectedException(typeof(CronFormatException))]
        public void TestInvalidCron(string cron)
        {
            CronExpressionParser.Parse(cron);
        }

        [TestMethod]
        [DataRow("59 59 23 29 2 * 2024", "2024-02-29 23:59:59", "2024-01-01 00:00:00")]
        [DataRow("0 0 12 ? * 2#1 *", "2025-01-07 12:00:00", "2025-01-01 00:00:00")] // The first Tuesday of every month at 12:00 PM
        [DataRow("0 0 15 ? * 5L *", "2025-01-31 15:00:00", "2025-01-01 00:00:00")] // The last Friday of every month at 3:00 PM
        [DataRow("0 0 1 1 1,4,7,10 * *", "2025-01-01 01:00:00", "2025-01-01 00:00:00")] // The first day of each quarter at 1:00 AM
        [DataRow("0 0 12 1 1,7 * *", "2025-01-01 12:00:00", "2025-01-01 00:00:00")] // January 1st and July 1st of every year at 12:00 PM
        [DataRow("0 15,45 * ? * 1-5 *", "2025-01-01 00:15:00", "2025-01-01 00:00:00")] // Every hour at the 15th and 45th minute from Monday to Friday
        [DataRow("0 59 23 31 12 ? *", "2025-12-31 23:59:00", "2025-01-01 00:00:00")] // The last day of every year at 11:59 PM
        [DataRow("0 0 10 ? 1 1#1 *", "2025-01-06 10:00:00", "2025-01-01 00:00:00")] // The first Monday of January at 10:00 AM
        [DataRow("0 0/30 8-18 ? * 2-6 *", "2025-01-01 08:00:00", "2025-01-01 00:00:00")] // Every half hour from 8:00 AM to 6:00 PM on weekdays
        [DataRow("0 59 23 L 3,6,9,12 ? *", "2025-03-31 23:59:00", "2025-01-01 00:00:00")] // The last day of March, June, September, and December at 11:59 PM
        public void TestValidCron(string cron, string expectedDate, string time)
        {
            var cronExpression = CronExpressionParser.Parse(cron);
            var nextTime = cronExpression.GetNextAvailableTime(DateTimeOffset.Parse(time));

            Assert.IsTrue(nextTime.HasValue);
            Assert.AreEqual(expectedDate, nextTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        [TestMethod]
        [DataRow("59 59 23 29 2 * 2024", new[] { "2024-02-29 23:59:59" }, "2024-01-01 00:00:00")]
        [DataRow("0 0 12 ? * 2#1 *", new[] { "2025-01-07 12:00:00", "2025-02-04 12:00:00", "2025-03-04 12:00:00", "2025-04-01 12:00:00" }, "2025-01-01 00:00:00")] // The first Tuesday of every month at 12:00 PM
        [DataRow("0 0 15 ? * 5L *", new[] { "2025-01-31 15:00:00", "2025-02-28 15:00:00", "2025-03-28 15:00:00", "2025-04-25 15:00:00" }, "2025-01-01 00:00:00")] // The last Friday of every month at 3:00 PM
        [DataRow("0 0 1 1 1,4,7,10 * *", new[] { "2025-01-01 01:00:00", "2025-04-01 01:00:00", "2025-07-01 01:00:00", "2025-10-01 01:00:00" }, "2025-01-01 00:00:00")] // The first day of each quarter at 1:00 AM
        [DataRow("0 0 12 1 1,7 * *", new[] { "2025-01-01 12:00:00", "2025-07-01 12:00:00", "2026-01-01 12:00:00", "2026-07-01 12:00:00" }, "2025-01-01 00:00:00")] // January 1st and July 1st of every year at 12:00 PM
        [DataRow("0 15,45 * ? * 1-5 *", new[] { "2025-01-01 00:15:00", "2025-01-01 00:45:00", "2025-01-01 01:15:00", "2025-01-01 01:45:00" }, "2025-01-01 00:00:00")] // Every hour at the 15th and 45th minute from Monday to Friday
        [DataRow("0 59 23 31 12 ? *", new[] { "2025-12-31 23:59:00", "2026-12-31 23:59:00", "2027-12-31 23:59:00", "2028-12-31 23:59:00" }, "2025-01-01 00:00:00")] // The last day of every year at 11:59 PM
        [DataRow("0 0 10 ? 1 1#1 *", new[] { "2025-01-06 10:00:00", "2026-01-05 10:00:00", "2027-01-04 10:00:00", "2028-01-03 10:00:00" }, "2025-01-01 00:00:00")] // The first Monday of January at 10:00 AM
        [DataRow("0 0/30 8-18 ? * 2-6 *", new[] {"2025-01-01 08:00:00", "2025-01-01 08:30:00", "2025-01-01 09:00:00", "2025-01-01 09:30:00"}, "2025-01-01 00:00:00")] // Every half hour from 8:00 AM to 6:00 PM on weekdays
        [DataRow("0 59 23 L 3,6,9,12 ? *", new[] { "2025-03-31 23:59:00", "2025-06-30 23:59:00", "2025-09-30 23:59:00", "2025-12-31 23:59:00" }, "2025-01-01 00:00:00")] // The last day of March, June, September, and December at 11:59 PM
        public void TestValidCron(string cron, string[] expectedDates, string time)
        {
            var cronExpression = CronExpressionParser.Parse(cron);
            var actualDates = cronExpression.GetNextAvailableTimes(DateTimeOffset.Parse(time), expectedDates.Length);

            for (int i = 0; i < expectedDates.Length; i++)
            {
                Assert.AreEqual(expectedDates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        [TestMethod]
        public void TestFromJson()
        {
            string json = File.ReadAllText("TestData/TestData.json");
            var tests = System.Text.Json.JsonSerializer.Deserialize<CronTest[]>(json);
            foreach (var test in tests)
            {
                var cronExpression = CronExpressionParser.Parse(test.Cron);
                var actualDates = cronExpression.GetNextAvailableTimes(new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero), test.Dates.Length);
                for (int i = 0; i < test.Dates.Length; i++)
                {
                    Assert.AreEqual(test.Dates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"), true, $"Cron: {test.Cron}");
                }
            }
        }

        [TestMethod]
        public void TestFromComplexJson()
        {
            string json = File.ReadAllText("TestData/ComplexCronData.json");
            var tests = System.Text.Json.JsonSerializer.Deserialize<CronTest[]>(json);
            foreach (var test in tests)
            {
                var cronExpression = CronExpressionParser.Parse(test.Cron);
                if(test.Dates.Length == 0)
                {
                    var actualDate = cronExpression.GetNextAvailableTime(new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero));
                    continue;
                }

                var actualDates = cronExpression.GetNextAvailableTimes(new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero), test.Dates.Length);
                for (int i = 0; i < test.Dates.Length; i++)
                {
                    Assert.AreEqual(test.Dates[i], actualDates[i].ToString("yyyy-MM-dd HH:mm:ss"), true, $"Cron: {test.Cron}");
                }
            }
        }
    }

    class CronTest
    {
        [JsonPropertyName("cron")]
        public string Cron { get; set; }

        [JsonPropertyName("dates")]
        public string[] Dates { get; set; }
    }
}
