<p align="center"><a href="./README.md">English</a> | 中文 </p>

# CronParser
| 包 | NuGet 稳定版 | 下载量 |
| ------- | ------------ | --------- |
| [CronParser](https://www.nuget.org/packages/CronParser/) | [![CronParser](https://img.shields.io/nuget/v/CronParser.svg)](https://www.nuget.org/packages/CronParser/) | [![CronParser](https://img.shields.io/nuget/dt/CronParser.svg)](https://www.nuget.org/packages/CronParser/) |


[![CI](https://github.com/zhurongbo111/CronParser/actions/workflows/CI.yml/badge.svg)](https://github.com/zhurongbo111/CronParser/actions/workflows/CI.yml)

## 介绍
`CronParser` 是一个用于解析 Cron 表达式的 C# 库。它可以将 Cron 表达式字符串转换为 `CronExpression` 对象，支持 5 部分（从分钟到星期几）、6 部分（从秒到星期几）和 7 部分（从秒到年份）的 Cron 表达式。

## 特性
- **灵活解析**：支持不同长度的 Cron 表达式。
- **输入验证**：严格验证输入的 Cron 表达式以确保其有效性。
- **错误处理**：在解析过程中捕获并处理潜在错误，增强代码的健壮性。

该库支持的 Cron 表达式版本如下：

###### 五部分格式

    * * * * *
    - - - - -
    | | | | |
    | | | | |
    | | | | +----- 星期几（范围：0-6 或 SUN-SAT）
    | | | +------- 月（范围：1-12 或 JAN-DEC）
    | | +--------- 日期（范围：1-31）
    | +----------- 小时（范围：0-23）
    +------------- 分钟（范围：0-59）

###### 六部分格式（包括秒）

    * * * * * *
    - - - - - -
    | | | | | |
    | | | | | |
    | | | | | +--- 星期几
    | | | | +----- 月
    | | | +------- 日期
    | | +--------- 小时
    | +----------- 分钟
    +------------- 秒（范围：0-59）

###### 七部分格式

    * * * * * * *
    - - - - - - -
    | | | | | | |
    | | | | | | |
    | | | | | | +--- 年
    | | | | | +----- 星期几
    | | | | +------- 月
    | | | +--------- 日期
    | | +----------- 小时
    | +------------- 分钟
    +--------------- 秒

#### Cron 表达式

|    字段     | 必需 | 允许值  | 允许的特殊字符 |                           备注                            |
| :----------: | :------: | :-------------: | :------------------------: | :----------------------------------------------------------: |
|    秒    |    否    |      0-59       |      `*` `,` `-`,`/`       | `*` 表示每秒 <br>`3-10` 指定从 3 到 10 秒。<br>`5/15` 指定秒：5,20,35,50<br>`3,5,7` 指定秒：3,5,7 |
|    分钟    |   是    |      0–59       |      `*` `,` `-`,`/`       |                     类似上面。                      |
|     小时     |   是    |      0–23       |      `*` `,` `-`,`/`       |                     类似上面。                      |
| 日期 |   是    |      1–31       |      `*` `,` `-`  `L`      |           `L` 指定月份的最后一天。           |
|    月     |   是    | 1–12 或 JAN–DEC |      `*` `,` `-` `/`       |                 类似秒。                  |
| 星期几  |   是    | 0–6 或 SUN–SAT  |  `*` `,` `-`  `/` `L` `#`  | `5L` 指定最后一个星期五。<br> `5#3` 指定月份的第三个星期五。 |
|     年     |    否    |    1970–2099    |      `*` `,` `-` `/`       |                     类似秒。                     |



```csharp
DateTimeOffset time = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);
var cronExpression = CronExpressionParser.Parse("0 * * * * *");
var nextTime = cronExpression.GetNextAvaliableTime(time);
Console.WriteLine(nextTime.ToString("yyyy-MM-dd HH:mm:ss"));//2025-01-01 00:01:00
```

## 

## 许可证

此项目根据 MIT 许可证授权 - 有关详细信息，请参阅 [LICENSE](LICENSE) 文件。

## 致谢

- [Cron 维基](https://en.wikipedia.org/wiki/Cron) 获取更多关于 cron 表达式的信息。
