using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Builder.Models
{
    /// <summary>
    /// Splunk options
    /// </summary>
    public class SplunkOptions
    {
        /// <summary>
        /// Set if splunk is enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Minimum Level
        /// </summary>
        public LogEventLevel? MinimumLevel { get; set; }

        /// <summary>
        /// Splunk index
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// Splunk Application
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        /// Process name
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// Company
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Product version
        /// </summary>
        public string ProductVersion { get; set; }

        /// <summary>
        /// Server Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Source type
        /// </summary>
        public string SourceType { get; set; }

        /// <summary>
        /// Access token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Custom Text Formatter
        /// </summary>
        public ITextFormatter textFormatter { get; set; }
    }
}
