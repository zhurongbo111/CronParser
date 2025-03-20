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
    }
}
