using Serilog.Builder.Extensions;
using Serilog.Builder.Models;
using Serilog.Builder.Models.Settings;
using System;

namespace Serilog.Builder
{
    public partial class LoggerBuilder : ILoggerBuilder
    {
        /// <summary>
        /// Build lapi configuration
        /// </summary>
        /// <param name="logger"></param>
        private void BuildLapi(LoggerConfiguration logger)
        {
            if (this.OutputConfiguration.Lapi.Enabled == true)
            {
                var logLevel = this.OutputConfiguration.Lapi.Options.MinimumLevel
                    ?? this.OutputConfiguration.MinimumLevel;

                var lapiSettings = this.GetLapiLogSettings();

                logger.WriteTo.Lapi(
                    requestUri: lapiSettings.ServerURL,
                    index: lapiSettings.Index,
                    restrictedToMinimumLevel: logLevel,
                    queueLimitBytes: lapiSettings.QueueLimitBytes,
                    logEventsInBatchLimit: lapiSettings.LogEventsInBatchLimit,
                    batchSizeLimitBytes: lapiSettings.BatchSizeLimitBytes);
            }
        }

        /// <summary>
        /// Enable lapi
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public LoggerBuilder EnableLapi(string url, string index)
        {
            return this.SetupLapi(new LapiOptions
            {
                Enabled = true,
                Url = url,
                Index = index
            });
        }

        /// <summary>
        /// Setup lapi
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public LoggerBuilder SetupLapi(LapiOptions options)
        {
            this.OutputConfiguration.Lapi.Options = options
                ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.Url) == true && options.Enabled == true)
            {
                throw new ArgumentNullException(nameof(options.Url));
            }

            this.OutputConfiguration.Lapi.Enabled = options.Enabled;

            return this;
        }

        /// <summary>
        /// Disable lapi
        /// </summary>
        /// <returns></returns>
        public LoggerBuilder DisableLapi()
        {
            this.OutputConfiguration.Lapi.Enabled = false;
            return this;
        }

        /// <summary>
        /// Get Lapi log settings
        /// </summary>
        /// <returns></returns>
        private LapiLogSettings GetLapiLogSettings()
        {
            var lapiLogSettings = new LapiLogSettings
            {
                Application = this.OutputConfiguration.Lapi.Options.Application,
                Index = this.OutputConfiguration.Lapi.Options.Index,
                ProcessName = this.OutputConfiguration.Lapi.Options.ProcessName,
                ProductCompany = this.OutputConfiguration.Lapi.Options.Company,
                ProductVersion = this.OutputConfiguration.Lapi.Options.ProductVersion,
                ServerURL = this.OutputConfiguration.Lapi.Options.Url,
                SourceType = this.OutputConfiguration.Lapi.Options.SourceType,
                QueueLimitBytes = this.OutputConfiguration.Lapi.Options.QueueLimitBytes,
                BatchSizeLimitBytes = this.OutputConfiguration.Lapi.Options.BatchSizeLimitBytes,
                LogEventsInBatchLimit = this.OutputConfiguration.Lapi.Options.LogEventsInBatchLimit
            };

            return lapiLogSettings;
        }
    }
}
