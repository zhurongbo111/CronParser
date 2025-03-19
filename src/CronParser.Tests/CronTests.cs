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

        [TestMethod] //Start  time: 2025-01-01 00:10:00
        [DataRow("0 10-13 * * * * *", new string[] { "2025-01-01 00:11:00", "2025-01-01 00:12:00", "2025-01-01 00:13:00", "2025-01-01 01:10:00"})]
        [DataRow("0 12-14 * * * * *", new string[] { "2025-01-01 00:12:00", "2025-01-01 00:13:00", "2025-01-01 00:14:00", "2025-01-01 01:12:00" })]
        [DataRow("0 2-4 * * * * *", new string[] { "2025-01-01 01:02:00", "2025-01-01 01:03:00", "2025-01-01 01:04:00", "2025-01-01 02:02:00" })]
        [DataRow("0 2,4 * * * * *", new string[] { "2025-01-01 01:02:00", "2025-01-01 01:04:00", "2025-01-01 02:02:00", "2025-01-01 02:04:00" })]
        [DataRow("0 2,14 * * * * *", new string[] { "2025-01-01 00:14:00", "2025-01-01 01:02:00", "2025-01-01 01:14:00", "2025-01-01 02:02:00" })]
        [DataRow("0 2/15 * * * * *", new string[] { "2025-01-01 00:17:00", "2025-01-01 00:32:00", "2025-01-01 00:47:00", "2025-01-01 01:02:00" })]
        [DataRow("0 10/20 * * * * *", new string[] { "2025-01-01 00:30:00", "2025-01-01 00:50:00", "2025-01-01 01:10:00", "2025-01-01 01:30:00" })]
        [DataRow("0 11/26 * * * * *", new string[] { "2025-01-01 00:11:00", "2025-01-01 00:37:00", "2025-01-01 01:11:00", "2025-01-01 01:37:00" })]
        public void TestMinuteToken(string cron, string[] expectedDates)
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

        [TestMethod]
        [DataRow("0 0 5-7 * * * *", new string[] { "2025-01-01 06:00:00", "2025-01-01 07:00:00", "2025-01-02 05:00:00", "2025-01-02 06:00:00" })]
        [DataRow("0 0 6-8 * * * *", new string[] { "2025-01-01 06:00:00", "2025-01-01 07:00:00", "2025-01-01 08:00:00", "2025-01-02 06:00:00" })]
        [DataRow("0 0 4-7 * * * *", new string[] { "2025-01-01 06:00:00", "2025-01-01 07:00:00", "2025-01-02 04:00:00", "2025-01-02 05:00:00" })]
        [DataRow("0 0 4-5 * * * *", new string[] { "2025-01-02 04:00:00", "2025-01-02 05:00:00", "2025-01-03 04:00:00", "2025-01-03 05:00:00" })]
        [DataRow("0 0 2-4 * * * *", new string[] { "2025-01-02 02:00:00", "2025-01-02 03:00:00", "2025-01-02 04:00:00", "2025-01-03 02:00:00" })]
        [DataRow("0 0 5,7 * * * *", new string[] { "2025-01-01 07:00:00", "2025-01-02 05:00:00", "2025-01-02 07:00:00", "2025-01-03 05:00:00" })]
        public void TestHourToken(string cron, string[] expectedDates)
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
