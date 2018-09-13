using Serilog.Events;

namespace Serilog.Builder.Models
{
    /// <summary>
    /// Console options
    /// </summary>
    public class ConsoleOptions
    {
        /// <summary>
        /// Minimum Level
        /// </summary>
        public LogEventLevel? MinimumLevel { get; set; }
    }
}
