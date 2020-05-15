using Serilog.Builder.Models;
using System;

namespace Serilog.Builder
{
    /// <summary>
    /// Serilog Logger builder
    /// </summary>
    public partial class LoggerBuilder : ILoggerBuilder
    {
        /// <summary>
        /// Build new relic configuration
        /// </summary>
        /// <param name="logger"></param>
        private void BuildNewRelic(LoggerConfiguration logger)
        {
            if (this.OutputConfiguration.NewRelic.Enabled == true)
            {
                var logLevel = this.OutputConfiguration.NewRelic.Options.MinimumLevel ?? this.OutputConfiguration.MinimumLevel;

                logger.WriteTo.NewRelicLogs(
                    applicationName: this.OutputConfiguration.NewRelic.Options.AppName,
                    licenseKey: this.OutputConfiguration.NewRelic.Options.LicenseKey,
                    restrictedToMinimumLevel: logLevel);
            }
        }

        /// <summary>
        /// Enable NewRelic
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public LoggerBuilder EnableNewRelic(string appName, string licenseKey)
        {
            return this.SetupNewRelic(new NewRelicOptions
            {
                Enabled = true,
                AppName = appName,
                LicenseKey = licenseKey
            });
        }

        /// <summary>
        /// Setup NewRelic
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public LoggerBuilder SetupNewRelic(NewRelicOptions options)
        {
            this.OutputConfiguration.NewRelic.Options = options
                ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.AppName) == true && options.Enabled == true)
            {
                throw new ArgumentNullException(nameof(options.AppName));
            }

            if (string.IsNullOrWhiteSpace(options.LicenseKey) == true && options.Enabled == true)
            {
                throw new ArgumentNullException(nameof(options.LicenseKey));
            }

            this.OutputConfiguration.NewRelic.Enabled = options.Enabled;

            return this;
        }

        /// <summary>
        /// Disable NewRelic
        /// </summary>
        /// <returns></returns>
        public LoggerBuilder DisableNewRelic()
        {
            this.OutputConfiguration.NewRelic.Enabled = false;
            return this;
        }
    }
}