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
    }
}
