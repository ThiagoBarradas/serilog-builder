using Serilog.Builder.Models;
using Xunit;

namespace Serilog.Builder.Tests
{
    public static class LoggerInitializeTests
    {
        [Fact]
        public static void Construct_Using_Default()
        {
            // arrage & act
            var loggerOptions = new LoggerOptions
            {
                Application = "Application",
                Domain = "Domain",
                IsDebugEnabled = false,
                Console = new ConsoleOptions(),
                Seq = new SeqOptions(),
                Splunk = new SplunkOptions(),
                NewRelic = new NewRelicOptions()
            };

            LoggerInitialize.Start(loggerOptions);

            // assert
            Assert.False(loggerOptions.IsDebugEnabled);
            Assert.False(loggerOptions.Console.Enabled);
            Assert.False(loggerOptions.Seq.Enabled);
            Assert.False(loggerOptions.Splunk.Enabled);
            Assert.False(loggerOptions.NewRelic.Enabled);
        }

        [Fact]
        public static void Construct_Using_Default_With_DebugEnabled()
        {
            // arrage & act
            var loggerOptions = new LoggerOptions
            {
                Application = "Application",
                Domain = "Domain",
                IsDebugEnabled = true,
                Console = new ConsoleOptions(),
                Seq = new SeqOptions(),
                Splunk = new SplunkOptions(),
                NewRelic = new NewRelicOptions()
            };

            LoggerInitialize.Start(loggerOptions);

            // assert
            Assert.True(loggerOptions.IsDebugEnabled);
            Assert.False(loggerOptions.Console.Enabled);
            Assert.False(loggerOptions.Seq.Enabled);
            Assert.False(loggerOptions.Splunk.Enabled);
            Assert.False(loggerOptions.NewRelic.Enabled);
        }
    }
}
