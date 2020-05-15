using Serilog.Events;

namespace Serilog.Builder.Models
{
    /// <summary>
    /// NewRelic options
    /// </summary>
    public class NewRelicOptions
    {
        /// <summary>
        /// Set if new relic is enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Minimum Level
        /// </summary>
        public LogEventLevel? MinimumLevel { get; set; }

        /// <summary>
        /// NewRelic App Name
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// NewRelic License Key
        /// </summary>
        public string LicenseKey { get; set; }
    }
}
