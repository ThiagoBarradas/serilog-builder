[![Build Status](https://barradas.visualstudio.com/Contributions/_apis/build/status/NugetPackage/Serilog%20Builder?branchName=master)](https://barradas.visualstudio.com/Contributions/_build/latest?definitionId=9&branchName=master)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/f204b3b917ce4c4783feaaaef649e152)](https://www.codacy.com/app/ThiagoBarradas/serilog-builder?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=ThiagoBarradas/serilog-builder&amp;utm_campaign=Badge_Grade)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Serilog.Builder.svg)](https://www.nuget.org/packages/Serilog.Builder/)
[![NuGet Version](https://img.shields.io/nuget/v/Serilog.Builder.svg)](https://www.nuget.org/packages/Serilog.Builder/)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=ThiagoBarradas_serilog-builder&metric=alert_status)](https://sonarcloud.io/dashboard?id=ThiagoBarradas_serilog-builder)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=ThiagoBarradas_serilog-builder&metric=coverage)](https://sonarcloud.io/dashboard?id=ThiagoBarradas_serilog-builder)

# Serilog.Builder

Builder with my serilog common settings for use Console, Seq and Splunk sinks.

# Sample

Suggested settings
```c#

LoggerBuilder builder = new LoggerBuilder();

SeqOptions seqOptions = new SeqOptions
{
    Enabled = true,
    Url = "http://localhost",
    ApiKey = "123456"
};

SplunkOptions splunkOptions = new SplunkOptions
{
    Enabled = true,
    Url = "http://localhost",
    Token = "123456",
    Index = "my.index"
};

NewRelicOptions newRelicOptions = new NewRelicOptions
{
    Enabled = true,
    AppName = "My App",
    LicenseKey = "xxxxx"
}; 

DataDogOptions dataDogOptions = new DataDogOptions
{
    Enabled = true,
    Service = "My App",
    ApiKey = "xxxxx"
}; 


Log.Logger = builder
    .UseSuggestedSetting("MyDomain", "MyApplication")
    .SetupSeq(seqOptions)
    .SetupSplunk(splunkOptions)
    .SetupNewRelic(newRelicOptions)
    .SetupDataDog(dataDogOptions)
    .BuildLogger();

Log.Logger.Debug("Log something");

```

Custom mode
```c#

LoggerBuilder builder = new LoggerBuilder();

Log.Logger = builder
    .EnableConsole()
    .EnableSeq("http://localhost")
    .EnableSplunk("http://localhost")
    .EnableNewRelic("My App")
    .EnableEnrichWithEnvironment()
    .AddEnrichProperty("Application", "MyApp")
    .SetMinimumLevel(LogEventLevel.Debug)
    .AddOverrideMinimumLevel("System", LogEventLevel.Error)
    .BuildLogger();

Log.Logger.Debug("Log something");

```

## Install via NuGet

```
PM> Install-Package Serilog.Builder
```

## How can I contribute?
Please, refer to [CONTRIBUTING](.github/CONTRIBUTING.md)

## Found something strange or need a new feature?
Open a new Issue following our issue template [ISSUE_TEMPLATE](.github/ISSUE_TEMPLATE.md)

## Changelog
See in [nuget version history](https://www.nuget.org/packages/Serilog.Builder)

## Did you like it? Please, make a donate :)

If you liked this project, please make a contribution and help to keep this and other initiatives, send me some Satochis.

BTC Wallet: `1G535x1rYdMo9CNdTGK3eG6XJddBHdaqfX`

![1G535x1rYdMo9CNdTGK3eG6XJddBHdaqfX](https://i.imgur.com/mN7ueoE.png)
