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
        /// Build seq configuration
        /// </summary>
        /// <param name="logger"></param>
        private void BuildSeq(LoggerConfiguration logger)
        {
            if (this.OutputConfiguration.Seq.Enabled == true)
            {
                var logLevel = this.OutputConfiguration.Seq.Options.MinimumLevel ?? this.OutputConfiguration.MinimumLevel;
                var seqUrl = this.OutputConfiguration.Seq.Options.Url;
                var seqApiKey = (string.IsNullOrWhiteSpace(this.OutputConfiguration.Seq.Options.ApiKey) == false)
                    ? this.OutputConfiguration.Seq.Options.ApiKey : null;

                logger.WriteTo.Seq(seqUrl, apiKey: seqApiKey, restrictedToMinimumLevel: logLevel);
            }
        }

        /// <summary>
        /// Enable seq
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public LoggerBuilder EnableSeq(string url)
        {
            return this.SetupSeq(new SeqOptions
            {
                Enabled = true,
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
            return this.SetupSeq(new SeqOptions
            {
                Enabled = true,
                Url = url,
                ApiKey = apiKey
            });
        }

        /// <summary>
        /// Setup seq
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public LoggerBuilder SetupSeq(SeqOptions options)
        {
            this.OutputConfiguration.Seq.Options = options
                ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.Url) == true && options.Enabled == true)
            {
                throw new ArgumentNullException(nameof(options.Url));
            }

            this.OutputConfiguration.Seq.Enabled = options.Enabled;

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
    }
}