using Serilog.Builder.Models;
using Serilog.Sinks.Splunk.CustomFormatter;
using System;

namespace Serilog.Builder
{
    /// <summary>
    /// Serilog Logger builder
    /// </summary>
    public partial class LoggerBuilder : ILoggerBuilder
    {
        /// <summary>
        /// Build splunk configuration
        /// </summary>
        /// <param name="logger"></param>
        private void BuildSplunk(LoggerConfiguration logger)
        {
            if (this.OutputConfiguration.Splunk.Enabled == true)
            {
                var logLevel = this.OutputConfiguration.Splunk.Options.MinimumLevel ?? this.OutputConfiguration.MinimumLevel;
                var splunkSettings = this.GetSplunkLogSettings();
                var splunkFormatter = new SplunkJsonFormatter(splunkSettings);

                logger.WriteTo.EventCollector(splunkSettings.ServerURL, splunkSettings.Token, jsonFormatter: splunkFormatter, restrictedToMinimumLevel: logLevel);
            }
        }

        /// <summary>
        /// Enable splunk
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public LoggerBuilder EnableSplunk(string url)
        {
            return this.SetupSplunk(new SplunkOptions
            {
                Enabled = true,
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
            return this.SetupSplunk(new SplunkOptions
            {
                Enabled = true,
                Url = url,
                Token = token
            });
        }

        /// <summary>
        /// Setup seq
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public LoggerBuilder SetupSplunk(SplunkOptions options)
        {
            this.OutputConfiguration.Splunk.Options = options
                ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.Url) == true && options.Enabled == true)
            { 
                throw new ArgumentNullException(nameof(options.Url));
            }

            this.OutputConfiguration.Splunk.Enabled = options.Enabled;

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