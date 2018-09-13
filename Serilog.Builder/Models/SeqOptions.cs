using Serilog.Events;

namespace Serilog.Builder.Models
{
    /// <summary>
    /// Seq output
    /// </summary>
    public class SeqOptions
    {
        /// <summary>
        /// Minimum Level
        /// </summary>
        public LogEventLevel? MinimumLevel { get; set; }

        /// <summary>
        /// Server url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// If applied, api key for authorization
        /// </summary>
        public string ApiKey { get; set; }
    }
}
