using Microsoft.Extensions.Options;
using Moq;
using Serilog.Builder.Factory;
using Serilog.Builder.Models;
using Serilog.Events;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Serilog.Builder.Tests
{
    public class LoggerFactoryTests : IDisposable
    {
        private TestOutputHelper TestOutputHelper { get; set; }
        private readonly Mock<IOptions<LoggerOptions>> _loggerOptions = new Mock<IOptions<LoggerOptions>>();
        private readonly Mock<IOptions<SeqOptions>> _seqOptions = new Mock<IOptions<SeqOptions>>();
        private readonly Mock<IOptions<SplunkOptions>> _splunkOptions = new Mock<IOptions<SplunkOptions>>();
        private readonly Mock<IOptions<GoogleCloudLoggingOptions>> _googleCloudLoggingOptions = new Mock<IOptions<GoogleCloudLoggingOptions>>();

        public LoggerFactoryTests(ITestOutputHelper testOutputHelper)
        {
            this.TestOutputHelper = testOutputHelper as TestOutputHelper;

            var faker = new Bogus.Faker();

            var loggerOptions = new LoggerOptions
            {
                Domain = faker.Lorem.Word(),
                Application = faker.Lorem.Word()
            };

            _loggerOptions.Setup(x => x.Value).Returns(loggerOptions);

            var seqOptions = new SeqOptions
            {
                Enabled = faker.Random.Bool(),
                MinimumLevel = faker.PickRandom<LogEventLevel>(),
                Url = faker.Internet.Url(),
                ApiKey = faker.Random.AlphaNumeric(8)
            };

            _seqOptions.Setup(x => x.Value).Returns(seqOptions);

            var splunkOptions = new SplunkOptions
            {
                Enabled = faker.Random.Bool(),
                MinimumLevel = faker.PickRandom<LogEventLevel>(),
                Index = faker.Random.AlphaNumeric(8),
                Application = faker.Lorem.Word(),
                ProcessName = faker.Lorem.Word(),
                Company = faker.Company.CompanyName(),
                ProductVersion = faker.Random.Int(1, 10).ToString(),
                Url = faker.Internet.Url(),
                SourceType = faker.Lorem.Word(),
                Token = faker.Random.AlphaNumeric(8)
            };

            _splunkOptions.Setup(x => x.Value).Returns(splunkOptions);

            var gcpOptions = new GoogleCloudLoggingOptions
            {
                Enabled = faker.Random.Bool(),
                ProjectId = faker.Random.AlphaNumeric(8),
                CertificatePath = faker.System.FilePath(),
                UseJsonOutput = faker.Random.Bool(),
                ResourceType = faker.Random.AlphaNumeric(8),
                Labels = new Dictionary<string, string>(),
                ResourceLabels = new Dictionary<string, string>()
            };

            _googleCloudLoggingOptions.Setup(x => x.Value).Returns(gcpOptions);
        }

        [Fact]
        public void Construct_Using_Default()
        {
            // arrage & act
            ILoggerFactory loggerFactory = new LoggerFactory(_loggerOptions.Object, _seqOptions.Object,
                _splunkOptions.Object, _googleCloudLoggingOptions.Object);

            // assert
            Assert.NotNull(loggerFactory);
        }

        [Fact]
        public void Return_Single_Instance()
        {
            // arrage & act
            ILoggerFactory loggerFactory = new LoggerFactory(_loggerOptions.Object, _seqOptions.Object,
                _splunkOptions.Object, _googleCloudLoggingOptions.Object);

            ILoggerDefault loggerDefault = loggerFactory.GetInstance();
            ILoggerDefault loggerDefault2 = loggerFactory.GetInstance();

            // assert
            Assert.Same(loggerDefault, loggerDefault2);
        }

        public void Dispose()
        {
            LoggerFactory.Flush();
        }
    }
}
