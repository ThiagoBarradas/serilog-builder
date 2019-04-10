using Serilog.Builder.Factory;
using System;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Serilog.Builder.Tests
{
    public class LoggerDefaultTests
    {
        private TestOutputHelper TestOutputHelper { get; set; }

        private ILogger Logger { get; set; }

        public LoggerDefaultTests(ITestOutputHelper testOutputHelper)
        {
            this.TestOutputHelper = testOutputHelper as TestOutputHelper;

            var logger = new LoggerConfiguration();
            logger.WriteTo.XunitTestOutput(this.TestOutputHelper);
            this.Logger = logger.CreateLogger();
        }

        [Fact]
        public void ErrorAsync_Should_Log_Message_With_Error_Level()
        {
            // arrage
            ILoggerDefault logger = new LoggerDefault(this.Logger);

            // act
            logger.ErrorAsync("some error").Wait();

            // assert
            Assert.Contains("[Error] some error", this.TestOutputHelper.Output);
        }

        [Fact]
        public void ErrorAsync_Should_Log_Message_And_Exception_With_Error_Level()
        {
            // arrage
            ILoggerDefault logger = new LoggerDefault(this.Logger);

            // act
            logger.ErrorAsync("other error with exception", new ArgumentNullException()).Wait();

            // assert
            var output = this.TestOutputHelper.Output.Replace("\r", "");
            Assert.Contains("[Error] other error with exception\nSystem.ArgumentNullException: Value cannot be null.", output);
        }

        [Fact]
        public void WarningAsync_Should_Log_Message_With_Warning_Level()
        {
            // arrage
            ILoggerDefault logger = new LoggerDefault(this.Logger);

            // act
            logger.WarningAsync("some warning").Wait();

            // assert
            Assert.Contains("[Warning] some warning", this.TestOutputHelper.Output);
        }

        [Fact]
        public void WarningAsync_Should_Log_Message_And_Exception_With_Warning_Level()
        {
            // arrage
            ILoggerDefault logger = new LoggerDefault(this.Logger);

            // act
            logger.WarningAsync("other warning with exception", new ArgumentNullException()).Wait();

            // assert
            var output = this.TestOutputHelper.Output.Replace("\r", "");
            Assert.Contains("[Warning] other warning with exception\nSystem.ArgumentNullException: Value cannot be null.", output);
        }


        [Fact]
        public void InfoAsync_Should_Log_Message_With_Info_Level()
        {
            // arrage
            ILoggerDefault logger = new LoggerDefault(this.Logger);

            // act
            logger.InfoAsync("some info").Wait();

            // assert
            Assert.Contains("[Information] some info", this.TestOutputHelper.Output);
        }

        [Fact]
        public void DebugAsync_Should_Log_Message_As_Debug()
        {
            // arrage
            ILoggerDefault logger = new LoggerDefault(this.Logger);

            // act
            logger.DebugAsync("some debug").Wait();

            // assert
            Assert.Contains("[Debug] some debug", this.TestOutputHelper.Output);
        }
    }
}
