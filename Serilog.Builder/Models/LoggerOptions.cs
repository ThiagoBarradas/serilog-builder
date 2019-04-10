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
        /// Console Options
        /// </summary>
        public ConsoleOptions ConsoleOptions { get; set; }

        /// <summary>
        /// Seq Options
        /// </summary>
        public SeqOptions SeqOptions { get; set; }

        /// <summary>
        /// Splunk Options
        /// </summary>
        public SplunkOptions SplunkOptions { get; set; }

        /// <summary>
        /// Google Cloud Logging Options
        /// </summary>
        public GoogleCloudLoggingOptions GoogleCloudLoggingOptions { get; set; }
    }
}
