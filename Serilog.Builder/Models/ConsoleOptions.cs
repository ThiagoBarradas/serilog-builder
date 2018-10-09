using Serilog.Events;

namespace Serilog.Builder.Models
{
    /// <summary>
    /// Console options
    /// </summary>
    public class ConsoleOptions
    {
        /// <summary>
        /// Set if console is enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Minimum Level
        /// </summary>
        public LogEventLevel? MinimumLevel { get; set; }
    }
}
