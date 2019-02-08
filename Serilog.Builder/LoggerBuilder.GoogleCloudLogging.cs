using Serilog.Builder.Models;
using Serilog.Sinks.GoogleCloudLogging;
using System;
using System.Diagnostics;

namespace Serilog.Builder
{
    /// <summary>
    /// Serilog Logger builder
    /// </summary>
    public partial class LoggerBuilder : ILoggerBuilder
    {
        /// <summary>
        /// Build google cloud logging configuration
        /// </summary>
        /// <param name="logger"></param>
        private void BuildGoogleCloudLogging(LoggerConfiguration logger)
        {
            if (this.OutputConfiguration.GoogleCloudLogging.Enabled == true)
            {
                if (!string.IsNullOrWhiteSpace(this.OutputConfiguration.GoogleCloudLogging.Options.CertificatePath))
                {
                    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", this.OutputConfiguration.GoogleCloudLogging.Options.CertificatePath);
                }

                try
                {
                    var config = new GoogleCloudLoggingSinkOptions(
                        this.OutputConfiguration.GoogleCloudLogging.Options.ProjectId,
                        resourceType: this.OutputConfiguration.GoogleCloudLogging.Options.ResourceType,
                        labels: this.OutputConfiguration.GoogleCloudLogging.Options.Labels,
                        resourceLabels: this.OutputConfiguration.GoogleCloudLogging.Options.ResourceLabels);

                    config.UseJsonOutput = this.OutputConfiguration.GoogleCloudLogging.Options.UseJsonOutput;

                    logger.WriteTo.GoogleCloudLogging(config);
                }
                catch(Exception e)
                {
                    Debug.WriteLine("Serilog: Error on Setup GoogleCloudLogging: ");
                    Debug.WriteLine(" Message: {0}", e.Message);
                    Console.WriteLine("Serilog: Error on Setup GoogleCloudLogging: ");
                    Console.WriteLine(" Message: {0}", e.Message);
                }
            }
        }

        /// <summary>
        /// Enable GoogleCloudLogging
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public LoggerBuilder EnableGoogleCloudLogging(string projectId)
        {
            return this.SetupGoogleCloudLogging(new GoogleCloudLoggingOptions
            {
                Enabled = true,
                ProjectId = projectId
            });
        }

        /// <summary>
        /// Enable GoogleCloudLogging
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="certificatePath"></param>
        /// <returns></returns>
        public LoggerBuilder EnableGoogleCloudLogging(string projectId, string certificatePath)
        {
            return this.SetupGoogleCloudLogging(new GoogleCloudLoggingOptions
            {
                Enabled = true,
                ProjectId = projectId,
                CertificatePath = certificatePath
            });
        }

        /// <summary>
        /// Setup GoogleCloudLogging
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public LoggerBuilder SetupGoogleCloudLogging(GoogleCloudLoggingOptions options)
        {
            this.OutputConfiguration.GoogleCloudLogging.Options = options
                ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.ProjectId) == true && options.Enabled == true)
            {
                throw new ArgumentNullException(nameof(options.ProjectId));
            }

            this.OutputConfiguration.GoogleCloudLogging.Enabled = options.Enabled;

            return this;
        }

        /// <summary>
        /// Disable GoogleCloudLogging
        /// </summary>
        /// <returns></returns>
        public LoggerBuilder DisableGoogleCloudLogging()
        {
            this.OutputConfiguration.GoogleCloudLogging.Enabled = false;
            return this;
        }
    }
}