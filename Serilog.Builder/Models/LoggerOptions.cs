namespace Serilog.Builder.Models
{
    /// <summary>
    /// Logger options class
    /// </summary>
    public class LoggerOptions
    {
        /// <summary>
        /// Application
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        /// Domain
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Is Debug Enabled
        /// </summary>
        public bool IsDebugEnabled { get; set; }

        /// <summary>
        /// Console
        /// </summary>
        public ConsoleOptions Console { get; set; }

        /// <summary>
        /// Seq
        /// </summary>
        public SeqOptions Seq { get; set; }

        /// <summary>
        /// Splunk
        /// </summary>
        public SplunkOptions Splunk { get; set; }

        /// <summary>
        /// NewRelic
        /// </summary>
        public NewRelicOptions NewRelic { get; set; }

        /// <summary>
        /// Google Cloud Logging
        /// </summary>
        public GoogleCloudLoggingOptions GoogleCloudLogging { get; set; }
    }
}
