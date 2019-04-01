using Serilog.Builder.Models;
using Serilog.Events;
using Serilog.Sinks.Splunk.CustomFormatter;
using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Serilog.Builder.Tests
{
    public class LoggerBuilderTests
    {
        private TestOutputHelper TestOutputHelper { get; set; }

        public LoggerBuilderTests(ITestOutputHelper testOutputHelper)
        {
            this.TestOutputHelper = testOutputHelper as TestOutputHelper;
        }

        [Fact]
        public void Construct_Using_Default()
        {
            // arrage & act
            LoggerBuilder builder = new LoggerBuilder();

            // assert
            Assert.False(builder.OutputConfiguration.Console.Enabled);
            Assert.False(builder.OutputConfiguration.Seq.Enabled);
            Assert.False(builder.OutputConfiguration.Splunk.Enabled);
            Assert.False(builder.OutputConfiguration.EnableEnrichWithEnvironment);
            Assert.Empty(builder.OutputConfiguration.EnrichProperties);
            Assert.Empty(builder.OutputConfiguration.OverrideMinimumLevel);
            Assert.Equal(default(LogEventLevel), builder.OutputConfiguration.MinimumLevel);
        }

        [Fact]
        public void EnableConsole()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            builder.EnableConsole();

            // assert
            Assert.True(builder.OutputConfiguration.Console.Enabled);
        }

        [Fact]
        public void EnableConsole_With_MinimumLevel()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            builder.SetupConsole(new ConsoleOptions { Enabled = true, MinimumLevel = LogEventLevel.Warning });
            var logger = builder.BuildLogger();

            // assert
            Assert.True(builder.OutputConfiguration.Console.Enabled);
            Assert.Equal(LogEventLevel.Warning, builder.OutputConfiguration.Console.Options.MinimumLevel);
        }

        [Fact]
        public void SetupConsole()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            builder.SetupConsole(new ConsoleOptions
            {
                Enabled = true
            });

            // assert
            Assert.True(builder.OutputConfiguration.Console.Enabled);
        }

        [Fact]
        public void SetupConsole_Should_Throws_Exception_When_Options_Is_Null()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.SetupConsole((ConsoleOptions)null));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: options", ex.Message);
        }

        [Fact]
        public void DisableConsole()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            builder.EnableConsole();

            // act
            builder.DisableConsole();

            // assert
            Assert.False(builder.OutputConfiguration.Console.Enabled);
        }

        [Fact]
        public void EnableSeq_With_Url()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            builder.EnableSeq("http://www.google.com");
            var logger = builder.BuildLogger();

            // assert
            Assert.True(builder.OutputConfiguration.Seq.Enabled);
            Assert.NotNull(builder.OutputConfiguration.Seq.Options.Url);
            Assert.Null(builder.OutputConfiguration.Seq.Options.ApiKey);
        }

        [Fact]
        public void EnableSeq_With_Url_And_ApiKey()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            builder.EnableSeq("http://www.google.com", "apikey");

            // assert
            Assert.True(builder.OutputConfiguration.Seq.Enabled);
            Assert.NotNull(builder.OutputConfiguration.Seq.Options.Url);
            Assert.NotNull(builder.OutputConfiguration.Seq.Options.ApiKey);
        }

        [Fact]
        public void EnableSeq_With_SeqOptions()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            builder.SetupSeq(new SeqOptions
            {
                Url = "http://www.google.com",
                Enabled = true,
                MinimumLevel = LogEventLevel.Information,
                ApiKey = "test"
            });
            var logger = builder.BuildLogger();

            // assert
            Assert.True(builder.OutputConfiguration.Seq.Enabled);
            Assert.NotNull(builder.OutputConfiguration.Seq.Options.Url);
            Assert.Equal("http://www.google.com", builder.OutputConfiguration.Seq.Options.Url);
            Assert.NotNull(builder.OutputConfiguration.Seq.Options.ApiKey);
            Assert.Equal("test", builder.OutputConfiguration.Seq.Options.ApiKey);
        }

        [Fact]
        public void EnableSeq_Should_Throws_Exception_When_Options_Is_Null()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();


            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.SetupSeq((SeqOptions)null));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: options", ex.Message);
        }

        [Fact]
        public void EnableSeq_Should_Throws_Exception_When_Options_Url_Is_Empty_String()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();


            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.SetupSeq(new SeqOptions { Url = "", Enabled = true }));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: Url", ex.Message);
        }

        [Fact]
        public void EnableSeq_Should_Throws_Exception_When_Option_Url_Is_Null()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();


            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.SetupSeq(new SeqOptions { Enabled = true }));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: Url", ex.Message);
        }

        [Fact]
        public void DisableSeq()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            builder.EnableSeq("http://www.google.com");

            // act
            builder.DisableSeq();

            // assert
            Assert.False(builder.OutputConfiguration.Seq.Enabled);
        }

        [Fact]
        public void EnableSplunk_With_Url()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            builder.EnableSplunk("http://www.google.com");
            var logger = builder.BuildLogger();

            // assert
            Assert.True(builder.OutputConfiguration.Splunk.Enabled);
            Assert.NotNull(builder.OutputConfiguration.Splunk.Options.Url);
            Assert.Null(builder.OutputConfiguration.Splunk.Options.Token);
            Assert.Null(builder.OutputConfiguration.Splunk.Options.TextFormatter);
        }

        [Fact]
        public void EnableSplunk_With_Url_And_Token()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            builder.EnableSplunk("http://www.google.com", "token");

            // assert
            Assert.True(builder.OutputConfiguration.Splunk.Enabled);
            Assert.NotNull(builder.OutputConfiguration.Splunk.Options.Url);
            Assert.NotNull(builder.OutputConfiguration.Splunk.Options.Token);
            Assert.Null(builder.OutputConfiguration.Splunk.Options.TextFormatter);
        }

        [Fact]
        public void EnableSplunk_With_SplunkOptions()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            builder.SetupSplunk(new SplunkOptions
            {
                Url = "http://www.google.com",
                Enabled = true,
                MinimumLevel = LogEventLevel.Verbose,
                Application = "app",
                Company = "company",
                Index = "index",
                ProcessName = "process",
                ProductVersion = "1.0",
                SourceType = "__json",
                Token = "XXXXX"
            });
            var logger = builder.BuildLogger();

            // assert
            Assert.True(builder.OutputConfiguration.Splunk.Enabled);
            Assert.True(builder.OutputConfiguration.Splunk.Options.Enabled);
            Assert.NotNull(builder.OutputConfiguration.Splunk.Options.Url);
            Assert.Equal("http://www.google.com", builder.OutputConfiguration.Splunk.Options.Url);
            Assert.NotNull(builder.OutputConfiguration.Splunk.Options.Token);
            Assert.Equal("XXXXX", builder.OutputConfiguration.Splunk.Options.Token);
            Assert.Null(builder.OutputConfiguration.Splunk.Options.TextFormatter);
        }

        [Fact]
        public void EnableSplunk_Should_Throws_Exception_When_Options_Is_Null()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();


            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.SetupSplunk((SplunkOptions)null));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: options", ex.Message);
        }

        [Fact]
        public void EnableSplunk_Should_Throws_Exception_When_Options_Url_Is_Empty_String()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();


            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.SetupSplunk(new SplunkOptions { Url = "", Enabled = true }));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: Url", ex.Message);
        }

        [Fact]
        public void EnableSplunk_Should_Throws_Exception_When_Option_Url_Is_Null()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();


            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.SetupSplunk(new SplunkOptions { Enabled = true }));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: Url", ex.Message);
        }

        [Fact]
        public void DisableSplunk()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            builder.EnableSplunk("http://www.google.com");

            // act
            builder.DisableSplunk();

            // assert
            Assert.False(builder.OutputConfiguration.Splunk.Enabled);
        }

        //[Fact]
        public void EnableGoogleCloudLogging_With_ProjectId()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "some");

            // act
            builder.EnableGoogleCloudLogging("project_id");
            var logger = builder.BuildLogger();

            // assert
            Assert.True(builder.OutputConfiguration.GoogleCloudLogging.Enabled);
            Assert.NotNull(builder.OutputConfiguration.GoogleCloudLogging.Options.ProjectId);
            Assert.Equal("project_id", builder.OutputConfiguration.GoogleCloudLogging.Options.ProjectId);
            Assert.Null(builder.OutputConfiguration.GoogleCloudLogging.Options.CertificatePath);
            Assert.Equal("some", Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"));
        }

        //[Fact]
        public void EnableGoogleCloudLogging_With_ProjectId_And_CertificatePath()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "some");

            // act
            builder.EnableGoogleCloudLogging("project_id", "C:\\Certificate.json");
            var logger = builder.BuildLogger();

            // assert
            Assert.True(builder.OutputConfiguration.GoogleCloudLogging.Enabled);
            Assert.NotNull(builder.OutputConfiguration.GoogleCloudLogging.Options.ProjectId);
            Assert.Equal("project_id", builder.OutputConfiguration.GoogleCloudLogging.Options.ProjectId);
            Assert.NotNull(builder.OutputConfiguration.GoogleCloudLogging.Options.CertificatePath);
            Assert.Equal("C:\\Certificate.json", builder.OutputConfiguration.GoogleCloudLogging.Options.CertificatePath);
            Assert.Equal("C:\\Certificate.json", Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"));
        }

        //[Fact]
        public void EnableGoogleCloudLogging_With_GoogleCloudLoggingOptions()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "some");

            // act
            builder.SetupGoogleCloudLogging(new GoogleCloudLoggingOptions
            {
                Enabled = true,
                ProjectId = "project_id",
                CertificatePath = Directory.GetCurrentDirectory() + "\\Certificate.json"

            });
            var logger = builder.BuildLogger();

            // assert
            Assert.True(builder.OutputConfiguration.GoogleCloudLogging.Enabled);
            Assert.NotNull(builder.OutputConfiguration.GoogleCloudLogging.Options.ProjectId);
            Assert.Equal("project_id", builder.OutputConfiguration.GoogleCloudLogging.Options.ProjectId);
            Assert.NotNull(builder.OutputConfiguration.GoogleCloudLogging.Options.CertificatePath);
            Assert.Equal(Directory.GetCurrentDirectory() + "\\Certificate.json", builder.OutputConfiguration.GoogleCloudLogging.Options.CertificatePath);
        }

        //[Fact]
        public void EnableGoogleCloudLogging_With_GoogleCloudLoggingOptions_And_Empty_CertificatePath()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "some");

            // act
            builder.SetupGoogleCloudLogging(new GoogleCloudLoggingOptions
            {
                Enabled = true,
                ProjectId = "project_id",
                CertificatePath = ""

            });
            var logger = builder.BuildLogger();

            // assert
            Assert.True(builder.OutputConfiguration.GoogleCloudLogging.Enabled);
            Assert.NotNull(builder.OutputConfiguration.GoogleCloudLogging.Options.ProjectId);
            Assert.Equal("project_id", builder.OutputConfiguration.GoogleCloudLogging.Options.ProjectId);
            Assert.NotNull(builder.OutputConfiguration.GoogleCloudLogging.Options.CertificatePath);
            Assert.Equal("some", builder.OutputConfiguration.GoogleCloudLogging.Options.CertificatePath);
        }

        [Fact]
        public void EnableGoogleCloudLogging_Should_Throws_Exception_When_Options_Is_Null()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.SetupGoogleCloudLogging((GoogleCloudLoggingOptions)null));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: options", ex.Message);
        }

        [Fact]
        public void EnableGoogleCloudLogging_Should_Throws_Exception_When_Options_ProjectId_Is_Empty_String()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();


            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.SetupGoogleCloudLogging(new GoogleCloudLoggingOptions { ProjectId = "", Enabled = true }));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: ProjectId", ex.Message);
        }

        [Fact]
        public void EnableGoogleCloudLogging_Should_Throws_Exception_When_Option_ProjectId_Is_Null()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();


            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.SetupGoogleCloudLogging(new GoogleCloudLoggingOptions { Enabled = true }));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: ProjectId", ex.Message);
        }

        [Fact]
        public void DisableGoogleCloudLogging()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            builder.EnableGoogleCloudLogging("project_Id");

            // act
            builder.DisableGoogleCloudLogging();

            // assert
            Assert.False(builder.OutputConfiguration.GoogleCloudLogging.Enabled);
        }

        [Fact]
        public void DisableAllOutputs()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            builder.EnableConsole()
                .EnableSeq("http://www.google.com")
                .EnableSplunk("http://www.google.com")
                .EnableGoogleCloudLogging("project_id");

            // act
            builder.DisableAllOutputs();

            // assert
            Assert.False(builder.OutputConfiguration.Console.Enabled);
            Assert.False(builder.OutputConfiguration.Seq.Enabled);
            Assert.False(builder.OutputConfiguration.Splunk.Enabled);
            Assert.False(builder.OutputConfiguration.GoogleCloudLogging.Enabled);
        }

        [Fact]
        public void EnableEnrichWithEnvironment()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            builder.EnableEnrichWithEnvironment();

            // assert
            Assert.True(builder.OutputConfiguration.EnableEnrichWithEnvironment);
        }

        [Fact]
        public void DisableEnrichWithEnvironment()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            builder.EnableEnrichWithEnvironment();

            // act
            builder.DisableEnrichWithEnvironment();

            // assert
            Assert.False(builder.OutputConfiguration.EnableEnrichWithEnvironment);
        }

        [Fact]
        public void AddEnrichProperty_Should_Throws_Exception_When_Key_Is_Null()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.AddEnrichProperty(null, null));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: key", ex.Message);
        }

        [Fact]
        public void AddEnrichProperty_Should_Throws_Exception_When_Key_Is_Empty()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.AddEnrichProperty("", null));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: key", ex.Message);
        }

        [Fact]
        public void AddEnrichProperty_Should_Throws_Exception_When_Value_Is_Null()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.AddEnrichProperty("SomeProperty", null));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void AddEnrichProperty_Should_Enrich_Properties()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            builder.AddEnrichProperty("PropertyToAdd", "MyProperty");

            // assert
            Assert.Single(builder.OutputConfiguration.EnrichProperties);
            Assert.NotNull(builder.OutputConfiguration.EnrichProperties["PropertyToAdd"]);
            Assert.Equal("MyProperty", builder.OutputConfiguration.EnrichProperties["PropertyToAdd"]);
        }

        [Fact]
        public void AddEnrichProperty_Should_Enrich_Properties_When_Property_Already_Exists()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            builder.AddEnrichProperty("PropertyToAdd", "MyProperty2");

            // act
            builder.AddEnrichProperty("PropertyToAdd", "MyProperty");

            // assert
            Assert.Single(builder.OutputConfiguration.EnrichProperties);
            Assert.NotNull(builder.OutputConfiguration.EnrichProperties["PropertyToAdd"]);
            Assert.Equal("MyProperty", builder.OutputConfiguration.EnrichProperties["PropertyToAdd"]);
        }

        [Fact]
        public void AddEnrichProperty_Should_Enrich_Properties_When_Other_Property_Exists()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            builder.AddEnrichProperty("PropertyToAdd2", "MyProperty2");

            // act
            builder.AddEnrichProperty("PropertyToAdd", "MyProperty");

            // assert
            Assert.Equal(2, builder.OutputConfiguration.EnrichProperties.Count);
            Assert.NotNull(builder.OutputConfiguration.EnrichProperties["PropertyToAdd"]);
            Assert.Equal("MyProperty", builder.OutputConfiguration.EnrichProperties["PropertyToAdd"]);
        }

        [Fact]
        public void RemoveEnrichProperty_Should_Throws_Exception_When_Key_Is_Null()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.RemoveEnrichProperty(null));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: key", ex.Message);
        }

        [Fact]
        public void RemoveEnrichProperty_Should_Throws_Exception_When_Key_Is_Empty()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.RemoveEnrichProperty(""));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: key", ex.Message);
        }

        [Fact]
        public void RemoveEnrichProperty_Should_Works_When_Property_Exists()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            builder.AddEnrichProperty("PropertyToAdd", "MyProperty");

            // act
            builder.RemoveEnrichProperty("PropertyToAdd");

            // assert
            Assert.Empty(builder.OutputConfiguration.EnrichProperties);
        }

        [Fact]
        public void RemoveEnrichProperty_Should_Works_When_Other_Property_Exists()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            builder.AddEnrichProperty("PropertyToAdd2", "MyProperty");
            builder.AddEnrichProperty("PropertyToAdd", "MyProperty2");

            // act
            builder.RemoveEnrichProperty("PropertyToAdd");

            // assert
            Assert.Single(builder.OutputConfiguration.EnrichProperties);
        }

        [Fact]
        public void RemoveEnrichProperty_Should_Works_When_Property_Not_Exists()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            builder.AddEnrichProperty("PropertyToAdd", "MyProperty");

            // act
            builder.RemoveEnrichProperty("PropertyToAdd2");

            // assert
            Assert.Single(builder.OutputConfiguration.EnrichProperties);
        }

        [Fact]
        public void SetMinimumLevel()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            builder.SetMinimumLevel(LogEventLevel.Information);

            // assert
            Assert.Equal(LogEventLevel.Information, builder.OutputConfiguration.MinimumLevel);
        }

        [Fact]
        public void AddOverrideMinimumLevel_Should_Throws_Exception_When_Namespace_Is_Null()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.AddOverrideMinimumLevel(null, LogEventLevel.Warning));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: _namespace", ex.Message);
        }

        [Fact]
        public void AddOverrideMinimumLevel_Should_Throws_Exception_When_Namespace_Is_Empty()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.AddOverrideMinimumLevel("", LogEventLevel.Fatal));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: _namespace", ex.Message);
        }
        
        [Fact]
        public void AddOverrideMinimumLevel_Should_Register_Override()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            builder.AddOverrideMinimumLevel("Namespace", LogEventLevel.Verbose);

            // assert
            Assert.Single(builder.OutputConfiguration.OverrideMinimumLevel);
            Assert.Equal(LogEventLevel.Verbose, builder.OutputConfiguration.OverrideMinimumLevel["Namespace"]);
        }

        [Fact]
        public void AddOverrideMinimumLevel_Should_Register_Override_When_Property_Already_Exists()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            builder.AddOverrideMinimumLevel("Namespace", LogEventLevel.Error);

            // act
            builder.AddOverrideMinimumLevel("Namespace", LogEventLevel.Verbose);

            // assert
            Assert.Single(builder.OutputConfiguration.OverrideMinimumLevel);
            Assert.Equal(LogEventLevel.Verbose, builder.OutputConfiguration.OverrideMinimumLevel["Namespace"]);
        }

        [Fact]
        public void AddOverrideMinimumLevel_Should_Register_Override_When_Other_Property_Already_Exists()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            builder.AddOverrideMinimumLevel("Namespace2", LogEventLevel.Error);

            // act
            builder.AddOverrideMinimumLevel("Namespace", LogEventLevel.Verbose);

            // assert
            Assert.Equal(2, builder.OutputConfiguration.OverrideMinimumLevel.Count);
            Assert.Equal(LogEventLevel.Verbose, builder.OutputConfiguration.OverrideMinimumLevel["Namespace"]);
        }

        [Fact]
        public void RemoveOverrideMinimumLevel_Should_Throws_Exception_When_Namespace_Is_Null()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.RemoveOverrideMinimumLevel(null));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: _namespace", ex.Message);
        }

        [Fact]
        public void RemoveOverrideMinimumLevel_Should_Throws_Exception_When_Namespace_Is_Empty()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.RemoveOverrideMinimumLevel(""));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: _namespace", ex.Message);
        }

        [Fact]
        public void RemoveOverrideMinimumLevel_Should_Works_When_Namespace_Exists()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            builder.AddOverrideMinimumLevel("Namespace", LogEventLevel.Warning);

            // act
            builder.RemoveOverrideMinimumLevel("Namespace");

            // assert
            Assert.Empty(builder.OutputConfiguration.OverrideMinimumLevel);
        }

        [Fact]
        public void RemoveOverrideMinimumLevel_Should_Works_When_Other_Namespace_Exists()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            builder.AddOverrideMinimumLevel("Namespace2", LogEventLevel.Warning);
            builder.AddOverrideMinimumLevel("Namespace", LogEventLevel.Error);

            // act
            builder.RemoveOverrideMinimumLevel("Namespace");

            // assert
            Assert.Single(builder.OutputConfiguration.OverrideMinimumLevel);
        }

        [Fact]
        public void RemoveOverrideMinimumLevel_Should_Works_When_Namespace_Not_Exists()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            builder.AddOverrideMinimumLevel("Namespace", LogEventLevel.Verbose);

            // act
            builder.RemoveOverrideMinimumLevel("Namespace2");

            // assert
            Assert.Single(builder.OutputConfiguration.OverrideMinimumLevel);
        }

        [Fact]
        public void UseSuggestedSetting_Should_Throws_Exception_When_Domain_Is_Null()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.UseSuggestedSetting(null, "something"));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: domain", ex.Message);
        }

        [Fact]
        public void UseSuggestedSetting_Should_Throws_Exception_When_Domain_Is_Empty()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.UseSuggestedSetting("", "something"));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: domain", ex.Message);
        }

        [Fact]
        public void UseSuggestedSetting_Should_Throws_Exception_When_Application_Is_Null()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.UseSuggestedSetting("something", null));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: application", ex.Message);
        }

        [Fact]
        public void UseSuggestedSetting_Should_Throws_Exception_When_Application_Is_Empty()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            Exception ex = Assert.Throws<ArgumentNullException>(() =>
                builder.UseSuggestedSetting("something", ""));

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: application", ex.Message);
        }

        [Fact]
        public void UseSuggestedSetting_Should_Create_Default()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            // act
            builder.UseSuggestedSetting("something", "xpto");

            // assert
            Assert.True(builder.OutputConfiguration.Console.Enabled);
            Assert.False(builder.OutputConfiguration.Seq.Enabled);
            Assert.False(builder.OutputConfiguration.Splunk.Enabled);
            Assert.True(builder.OutputConfiguration.EnableEnrichWithEnvironment);
            Assert.Equal(2, builder.OutputConfiguration.EnrichProperties.Count);
            Assert.Equal("something", builder.OutputConfiguration.EnrichProperties["Domain"]);
            Assert.Equal("xpto", builder.OutputConfiguration.EnrichProperties["Application"]);
            Assert.Equal(2, builder.OutputConfiguration.OverrideMinimumLevel.Count);
            Assert.Equal(LogEventLevel.Warning, builder.OutputConfiguration.OverrideMinimumLevel["Microsoft"]);
            Assert.Equal(LogEventLevel.Error, builder.OutputConfiguration.OverrideMinimumLevel["System"]);
            Assert.Equal(LogEventLevel.Debug, builder.OutputConfiguration.MinimumLevel);
        }

        [Fact]
        public void Build_Complete_Logger()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            builder
                .UseSuggestedSetting("something", "xpto")
                .AddEnrichProperty("Other", "other")
                .SetupSeq(new SeqOptions
                {
                    Url = "http://www.google.com",
                    MinimumLevel = LogEventLevel.Fatal,
                    Enabled = true
                })
                .SetupSplunk(new SplunkOptions
                {
                    Url = "http://www.google.ocm",
                    MinimumLevel = LogEventLevel.Error,
                    Enabled = true
                });

            var loggerConfiguration = builder.BuildConfiguration();
            loggerConfiguration.WriteTo.XunitTestOutput(this.TestOutputHelper);
            var logger = loggerConfiguration.CreateLogger();
            
            // act
            logger.Debug("[{Domain}] [{Application}] [{MachineName}] [{Other}] Build_Complete_Logger");

            // assert
            Assert.Contains("[Debug] [\"something\"] [\"xpto\"] [\"" + Environment.MachineName + "\"] [\"other\"] Build_Complete_Logger", this.TestOutputHelper.Output);
            Assert.True(builder.OutputConfiguration.Console.Enabled);
            Assert.True(builder.OutputConfiguration.Seq.Enabled);
            Assert.True(builder.OutputConfiguration.Splunk.Enabled);
            Assert.True(builder.OutputConfiguration.EnableEnrichWithEnvironment);
            Assert.Equal(3, builder.OutputConfiguration.EnrichProperties.Count);
            Assert.Equal("something", builder.OutputConfiguration.EnrichProperties["Domain"]);
            Assert.Equal("xpto", builder.OutputConfiguration.EnrichProperties["Application"]);
            Assert.Equal("other", builder.OutputConfiguration.EnrichProperties["Other"]);
            Assert.Equal(2, builder.OutputConfiguration.OverrideMinimumLevel.Count);
            Assert.Equal(LogEventLevel.Warning, builder.OutputConfiguration.OverrideMinimumLevel["Microsoft"]);
            Assert.Equal(LogEventLevel.Error, builder.OutputConfiguration.OverrideMinimumLevel["System"]);
            Assert.Equal(LogEventLevel.Debug, builder.OutputConfiguration.MinimumLevel);
            Assert.False(builder.OutputConfiguration.GoogleCloudLogging.Enabled);
        }

        [Fact]
        public void Build_Default_Logger()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();
            builder.SetMinimumLevel(LogEventLevel.Verbose).BuildConfiguration();

            var loggerConfiguration = builder.BuildConfiguration();
            loggerConfiguration.WriteTo.XunitTestOutput(this.TestOutputHelper);
            var logger = loggerConfiguration.CreateLogger();
            
            // act
            logger.Verbose("SomeTest Build_Default_Logger");

            // assert
            Assert.Contains("SomeTest Build_Default_Logger", this.TestOutputHelper.Output);
            Assert.False(builder.OutputConfiguration.Console.Enabled);
            Assert.False(builder.OutputConfiguration.Seq.Enabled);
            Assert.False(builder.OutputConfiguration.GoogleCloudLogging.Enabled);
            Assert.False(builder.OutputConfiguration.Splunk.Enabled);
            Assert.False(builder.OutputConfiguration.EnableEnrichWithEnvironment);
            Assert.Empty(builder.OutputConfiguration.EnrichProperties);
            Assert.Empty(builder.OutputConfiguration.OverrideMinimumLevel);
            Assert.Equal(LogEventLevel.Verbose, builder.OutputConfiguration.MinimumLevel);
        }

        [Fact]
        public void Build_Basics_Logger()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            SeqOptions seqOptions = new SeqOptions
            {
                Url = "http://localhost",
                ApiKey = "123456"
            };

            SplunkOptions splunkOptions = new SplunkOptions
            {
                Url = "http://localhost",
                Token = "123456",
                Index = "my.index"
            };

            Log.Logger = builder
                .UseSuggestedSetting("MyDomain", "MyApplication")
                .SetupSeq(seqOptions)
                .SetupSplunk(splunkOptions)
                .BuildLogger();
            
            // act
            var logger = builder.BuildLogger();

            // assert
            Assert.NotNull(logger);
        }

        [Fact]
        public void Build_Basics_Logger_With_Json_Formatter()
        {
            // arrage
            LoggerBuilder builder = new LoggerBuilder();

            SeqOptions seqOptions = new SeqOptions
            {
                Url = "http://localhost",
                ApiKey = "123456"
            };

            SplunkOptions splunkOptions = new SplunkOptions
            {
                Url = "http://localhost",
                Token = "123456",
                Index = "my.index"
            };

            var splunkSettings = new SplunkLogSettings()
            {
                ServerURL = splunkOptions.Url,
                Token = splunkOptions.Token,
                Index = splunkOptions.Index
            };

            splunkOptions.TextFormatter = new SplunkJsonFormatter(splunkSettings);

            Log.Logger = builder
                .UseSuggestedSetting("MyDomain", "MyApplication")
                .SetupSeq(seqOptions)
                .SetupSplunk(splunkOptions)
                .BuildLogger();

            // act
            var logger = builder.BuildLogger();

            // assert
            Assert.NotNull(logger);
        }        
    }
}
