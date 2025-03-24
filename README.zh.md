<p align="center"><a href="./README.md">Emglish</a> | ���� </p>

# CronParser
| �� | NuGet �ȶ��� | ������ |
| ------- | ------------ | --------- |
| [CronParser](https://www.nuget.org/packages/CronParser/) | [![CronParser](https://img.shields.io/nuget/v/CronParser.svg)](https://www.nuget.org/packages/CronParser/) | [![CronParser](https://img.shields.io/nuget/dt/CronParser.svg)](https://www.nuget.org/packages/Quartz.NetCore.DependencyInjection/) |


[![CI](https://github.com/zhurongbo111/CronParser/actions/workflows/CI.yml/badge.svg)](https://github.com/zhurongbo111/CronParser/actions/workflows/CI.yml)

## ����
`CronParser` ��һ�����ڽ��� Cron ���ʽ�� C# �⡣�����Խ� Cron ���ʽ�ַ���ת��Ϊ `CronExpression` ����֧�� 5 ���֣��ӷ��ӵ����ڼ�����6 ���֣����뵽���ڼ����� 7 ���֣����뵽��ݣ��� Cron ���ʽ��

## ����
- **������**��֧�ֲ�ͬ���ȵ� Cron ���ʽ��
- **������֤**���ϸ���֤����� Cron ���ʽ��ȷ������Ч�ԡ�
- **������**���ڽ��������в��񲢴���Ǳ�ڴ�����ǿ����Ľ�׳�ԡ�

�ÿ�֧�ֵ� Cron ���ʽ�汾���£�

###### �岿�ָ�ʽ

    * * * * *
    - - - - -
    | | | | |
    | | | | |
    | | | | +----- ���ڼ�����Χ��0-6 �� SUN-SAT��
    | | | +------- �£���Χ��1-12 �� JAN-DEC��
    | | +--------- ���ڣ���Χ��1-31��
    | +----------- Сʱ����Χ��0-23��
    +------------- ���ӣ���Χ��0-59��

###### �����ָ�ʽ�������룩

    * * * * * *
    - - - - - -
    | | | | | |
    | | | | | |
    | | | | | +--- ���ڼ�
    | | | | +----- ��
    | | | +------- ����
    | | +--------- Сʱ
    | +----------- ����
    +------------- �루��Χ��0-59��

###### �߲��ָ�ʽ

    * * * * * * *
    - - - - - - -
    | | | | | | |
    | | | | | | |
    | | | | | | +--- ��
    | | | | | +----- ���ڼ�
    | | | | +------- ��
    | | | +--------- ����
    | | +----------- Сʱ
    | +------------- ����
    +--------------- ��

#### Cron ���ʽ

|    �ֶ�     | ���� | ����ֵ  | ����������ַ� |                           ��ע                            |
| :----------: | :------: | :-------------: | :------------------------: | :----------------------------------------------------------: |
|    ��    |    ��    |      0-59       |      `*` `,` `-`,`/`       | `*` ��ʾÿ�� <br>`3-10` ָ���� 3 �� 10 �롣<br>`5/15` ָ���룺5,20,35,50<br>`3,5,7` ָ���룺3,5,7 |
|    ����    |   ��    |      0�C59       |      `*` `,` `-`,`/`       |                     �������档                      |
|     Сʱ     |   ��    |      0�C23       |      `*` `,` `-`,`/`       |                     �������档                      |
| ���� |   ��    |      1�C31       |      `*` `,` `-`  `L`      |           `L` ָ���·ݵ����һ�졣           |
|    ��     |   ��    | 1�C12 �� JAN�CDEC |      `*` `,` `-` `/`       |                 �����롣                  |
| ���ڼ�  |   ��    | 0�C6 �� SUN�CSAT  |  `*` `,` `-`  `/` `L` `#`  | `5L` ָ�����һ�������塣<br> `5#3` ָ���·ݵĵ����������塣 |
|     ��     |    ��    |    1970�C2099    |      `*` `,` `-` `/`       |                     �����롣                     |



```csharp
DateTimeOffset time = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);
var cronExpression = CronExpressionParser.Parse("0 * * * * *");
var nextTime = cronExpression.GetNextAvaliableTime(time);
Console.WriteLine(nextTime.ToString("yyyy-MM-dd HH:mm:ss"));//2025-01-01 00:01:00
```

## 

## ���֤

����Ŀ���� MIT ���֤��Ȩ - �й���ϸ��Ϣ������� [LICENSE](LICENSE) �ļ���

## ��л

- [Cron ά��](https://en.wikipedia.org/wiki/Cron) ��ȡ������� cron ���ʽ����Ϣ��
