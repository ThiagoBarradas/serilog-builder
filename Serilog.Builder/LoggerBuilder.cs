using Serilog.Builder.Models;
using Serilog.Events;
using Serilog.Sinks.Splunk.CustomFormatter;
using System;

namespace Serilog.Builder
{
    /// <summary>
    /// Serilog Logger builder
    /// </summary>
    public class LoggerBuilder : ILoggerBuilder
    {
        /// <summary>
        /// Output configuration
        /// </summary>
        internal OutputConfiguration OutputConfiguration { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public LoggerBuilder()
        {
            this.OutputConfiguration = new OutputConfiguration();
        }

        /// <summary>
        /// Use suggested setting. This method reset all setting before apply suggested configurations
        /// - enable console
        /// - add enrich with environment
        /// - enrich with domain and application
        /// - minimum level : debug
        /// - override Microsoft/warning and System/error
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="application"></param>
        /// <returns></returns>
        public LoggerBuilder UseSuggestedSetting(string domain, string application)
        {
            if (string.IsNullOrWhiteSpace(domain) == true)
            {
                throw new ArgumentNullException(nameof(domain));
            }

            if (string.IsNullOrWhiteSpace(application) == true)
            {
                throw new ArgumentNullException(nameof(application));
            }

            this.OutputConfiguration = new OutputConfiguration();

            this.EnableConsole();
            this.EnableEnrichWithEnvironment();
            this.SetMinimumLevel(LogEventLevel.Debug);

            this.AddOverrideMinimumLevel("Microsoft", LogEventLevel.Warning);
            this.AddOverrideMinimumLevel("System", LogEventLevel.Error);

            this.AddEnrichProperty("Domain", domain);
            this.AddEnrichProperty("Application", application);

            return this;
        }

        /// <summary>
        /// Enable console
        /// </summary>
        /// <returns></returns>
        public LoggerBuilder EnableConsole()
        {
            this.OutputConfiguration.Console.Enabled = true;
            return this;
        }

        /// <summary>
        /// Disable console
        /// </summary>
        /// <returns></returns>
        public LoggerBuilder DisableConsole()
        {
            this.OutputConfiguration.Console.Enabled = false;
            return this;
        }

        /// <summary>
        /// Enable seq
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public LoggerBuilder EnableSeq(string url)
        {
            return this.EnableSeq(new SeqOptions
            {
                Url = url
            });
        }

        /// <summary>
        /// Enable seq
        /// </summary>
        /// <param name="url"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public LoggerBuilder EnableSeq(string url, string apiKey)
        {
            return this.EnableSeq(new SeqOptions
            {
                Url = url, 
                ApiKey = apiKey
            });
        }

        /// <summary>
        /// Enable seq
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public LoggerBuilder EnableSeq(SeqOptions options)
        {
            this.OutputConfiguration.Seq.Enabled = true;
            this.OutputConfiguration.Seq.Options =  options 
                ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.Url) == true)
            {
                throw new ArgumentNullException(nameof(options.Url));
            }
            return this;
        }

        /// <summary>
        /// Disable seq
        /// </summary>
        /// <returns></returns>
        public LoggerBuilder DisableSeq()
        {
            this.OutputConfiguration.Seq.Enabled = false;
            return this;
        }

        /// <summary>
        /// Enable splunk
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public LoggerBuilder EnableSplunk(string url)
        {
            return this.EnableSplunk(new SplunkOptions
            {
                Url = url
            });
        }

        /// <summary>
        /// Enable splunk
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public LoggerBuilder EnableSplunk(string url, string token)
        {
            return this.EnableSplunk(new SplunkOptions
            {
                Url = url,
                Token = token
            });
        }

        /// <summary>
        /// Enable seq
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public LoggerBuilder EnableSplunk(SplunkOptions options)
        {
            this.OutputConfiguration.Splunk.Enabled = true;
            this.OutputConfiguration.Splunk.Options = options
                ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.Url) == true)
            { 
                throw new ArgumentNullException(nameof(options.Url));
            }
            return this;
        }

        /// <summary>
        /// Disable splunk
        /// </summary>
        /// <returns></returns>
        public LoggerBuilder DisableSplunk()
        {
            this.OutputConfiguration.Splunk.Enabled = false;
            return this;
        }

        /// <summary>
        /// Disable all
        /// </summary>
        /// <returns></returns>
        public LoggerBuilder DisableAllOutputs()
        {
            return this.DisableConsole()
                       .DisableSeq()
                       .DisableSplunk();
        }

        /// <summary>
        /// Enrich with environment properties
        /// <returns></returns>
        public LoggerBuilder EnableEnrichWithEnvironment()
        {
            this.OutputConfiguration.EnableEnrichWithEnvironment = true;
            return this;
        }

        /// <summary>
        /// Disable enrich with environment properties
        /// <returns></returns>
        public LoggerBuilder DisableEnrichWithEnvironment()
        {
            this.OutputConfiguration.EnableEnrichWithEnvironment = false;
            return this;
        }

        /// <summary>
        /// Enrich properties
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public LoggerBuilder AddEnrichProperty(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key) == true)
            {
                throw new ArgumentNullException(nameof(key));
            }

            this.OutputConfiguration.EnrichProperties[key] = value
                ?? throw new ArgumentNullException(nameof(value));

            return this;
        }

        /// <summary>
        /// Remove some enrich
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public LoggerBuilder RemoveEnrichProperty(string key)
        {
            if (string.IsNullOrWhiteSpace(key) == true)
            {
                throw new ArgumentNullException(nameof(key));
            }

            this.OutputConfiguration.EnrichProperties.Remove(key);
            return this;
        }

        /// <summary>
        /// Set MinimumLevel
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public LoggerBuilder SetMinimumLevel(LogEventLevel level)
        {
            this.OutputConfiguration.MinimumLevel = level;
            return this;
        }

        /// <summary>
        /// Override MinimumLevel by namespace
        /// </summary>
        /// <param name="_namespace"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public LoggerBuilder AddOverrideMinimumLevel(string _namespace, LogEventLevel level)
        {
            if (string.IsNullOrWhiteSpace(_namespace) == true)
            {
                throw new ArgumentNullException(nameof(_namespace));
            }

            this.OutputConfiguration.OverrideMinimumLevel[_namespace] = level;
            return this;
        }

        /// <summary>
        /// Remove Override MinimumLevel by namespace
        /// </summary>
        /// <param name="_namespace"></param>
        /// <returns></returns>
        public LoggerBuilder RemoveOverrideMinimumLevel(string _namespace)
        {
            if (string.IsNullOrWhiteSpace(_namespace) == true)
            {
                throw new ArgumentNullException(nameof(_namespace));
            }
            
            this.OutputConfiguration.OverrideMinimumLevel.Remove(_namespace);
            return this;
        }

        /// <summary>
        /// Build Logger
        /// </summary>
        /// <returns></returns>
        public ILogger BuildLogger()
        {
            return this.BuildConfiguration().CreateLogger();
        }

        /// <summary>
        /// Build configuration
        /// </summary>
        /// <returns></returns>
        public LoggerConfiguration BuildConfiguration()
        {
            var logger = new LoggerConfiguration();
            logger.MinimumLevel.Is(this.OutputConfiguration.MinimumLevel);

            foreach (var overrideMinimumLevel in this.OutputConfiguration.OverrideMinimumLevel)
            {
                logger.MinimumLevel.Override(overrideMinimumLevel.Key, overrideMinimumLevel.Value);
            }

            if (this.OutputConfiguration.EnableEnrichWithEnvironment)
            {
                logger.Enrich.WithMachineName();
                logger.Enrich.WithEnvironmentUserName();
                logger.Enrich.FromLogContext();
            }

            foreach (var enrichProperty in this.OutputConfiguration.EnrichProperties)
            {
                logger.Enrich.WithProperty(enrichProperty.Key, enrichProperty.Value);
            }

            if (this.OutputConfiguration.Console.Enabled == true)
            {
                var logLevel = this.OutputConfiguration.Console.Options.MinimumLevel ?? this.OutputConfiguration.MinimumLevel;
                logger.WriteTo.Console(restrictedToMinimumLevel: logLevel);
            }

            if (this.OutputConfiguration.Seq.Enabled == true)
            {
                var logLevel = this.OutputConfiguration.Seq.Options.MinimumLevel ?? this.OutputConfiguration.MinimumLevel;
                var seqUrl = this.OutputConfiguration.Seq.Options.Url;
                var seqApiKey = (string.IsNullOrWhiteSpace(this.OutputConfiguration.Seq.Options.ApiKey) == false)
                    ? this.OutputConfiguration.Seq.Options.ApiKey : null;

                logger.WriteTo.Seq(seqUrl, apiKey: seqApiKey, restrictedToMinimumLevel: logLevel);
            }

            if (this.OutputConfiguration.Splunk.Enabled == true)
            {
                var logLevel = this.OutputConfiguration.Splunk.Options.MinimumLevel ?? this.OutputConfiguration.MinimumLevel;
                var splunkSettings = this.GetSplunkLogSettings();
                var splunkFormatter = new SplunkJsonFormatter(splunkSettings);

                logger.WriteTo.EventCollector(splunkSettings.ServerURL, splunkSettings.Token, jsonFormatter: splunkFormatter, restrictedToMinimumLevel: logLevel);
            }

            return logger;
        }

        /// <summary>
        /// Get Splunk log settings
        /// </summary>
        /// <returns></returns>
        private SplunkLogSettings GetSplunkLogSettings()
        {
            var splunkLogSettings = new SplunkLogSettings
            {
                Application = this.OutputConfiguration.Splunk.Options.Application,
                Index = this.OutputConfiguration.Splunk.Options.Index,
                ProcessName = this.OutputConfiguration.Splunk.Options.ProcessName,
                ProductCompany = this.OutputConfiguration.Splunk.Options.Company,
                ProductVersion = this.OutputConfiguration.Splunk.Options.ProductVersion,
                ServerURL = this.OutputConfiguration.Splunk.Options.Url,
                SourceType = this.OutputConfiguration.Splunk.Options.SourceType,
                Token = this.OutputConfiguration.Splunk.Options.Token
            };

            return splunkLogSettings;
        }
    }
}