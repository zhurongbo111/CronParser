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
        public void TestEveryToken(string cron, string expectedDate)
        {
            DateTimeOffset time = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var cronExpression = CronExpressionParser.Parse(cron);
            var nextTime = cronExpression.GetNextAvaliableTime(time);
            
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
    }
}
