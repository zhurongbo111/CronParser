# CronParser

Cron is most suitable for scheduling repetitive tasks. You can visit [Cron Wiki](https://en.wikipedia.org/wiki/Cron)  for more info.
This library provides the following facilities:

* Parsing of cron expression
* Calculation of occurrences of time based on a cron expression

The corn expression version supported by this library are following:

###### Five-part format

    * * * * *
    - - - - -
    | | | | |
    | | | | |
    | | | | +----- Day of week(Range:0-6 or SUN-SAT)
    | | | +------- Month(Range: 1-12 or JAN-DEC)
    | | +--------- Day of month(Range:1-31)
    | +----------- Hour(Range:0-23)
    +------------- Minute(Range:0-59)

###### Six-part format(include second)

    * * * * * *
    - - - - - -
    | | | | | |
    | | | | | |
    | | | | | +--- Day of week
    | | | | +----- Month
    | | | +------- Day of month
    | | +--------- Hour
    | +----------- Minute
    +------------- Second(Range:0-59)

###### Six-part format(include year)

    * * * * * *
    - - - - - -
    | | | | | |
    | | | | | |
    | | | | | +--- Year(Rang:1970-2099)
    | | | | +----- Day of week
    | | | +------- Month
    | | +--------- Day of month
    | +----------- Hour
    +------------- Minute

###### Seven-part format

    * * * * * * *
    - - - - - - -
    | | | | | | |
    | | | | | | |
    | | | | | | +--- Year
    | | | | | +----- Day of week
    | | | | +------- Month
    | | | +--------- Day of month
    | | +----------- Hour
    | +------------- Minute
    +--------------- Second

#### Cron expression

|    Field     | Required | Allowed values  | Allowed special characters |                           Remarks                            |
| :----------: | :------: | :-------------: | :------------------------: | :----------------------------------------------------------: |
|    Second    |    No    |      0-59       |      `*` `,` `-`,`/`       | `*` stands every second <br>`3-10` specifies the seconds from 3 to 10.<br>`5/15` specifies the seconds: 5,20,35,50<br>`3,5,7` specifies the seconds: 3,5,7 |
|    Minute    |   Yes    |      0–59       |      `*` `,` `-`,`/`       |                     Similar with above.                      |
|     Hour     |   Yes    |      0–23       |      `*` `,` `-`,`/`       |                     Similar with above.                      |
| Day of month |   Yes    |      1–31       |      `*` `,` `-`  `L`      |           `L` specifies the last day of the month.           |
|    Month     |   Yes    | 1–12 or JAN–DEC |        `*` `,` `-`         |                 Similar with second second.                  |
| Day of week  |   Yes    | 0–6 or SUN–SAT  |    `*` `,` `-`  `L` `#`    | `5L` specifies the last Friday.<br> `5#3` specifies the third Friday of the month. |
|     Year     |    No    |    1970–2099    |        `*` `,` `-`         |                 Similar with second second.                  |



```csharp
var s = CrontabSchedule.Parse("0,30 * * * * *",
                              new CrontabSchedule.ParseOptions
                              {
                                  IncludingSeconds = true
                              });
```
