namespace Serilog.Builder.Models
{
    /// <summary>
    /// GoogleCloudLogging Options
    /// </summary>
    public class GoogleCloudLoggingOptions
    {
        /// <summary>
        /// Set if GoogleCloudLogging is enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// GoogleCloudLogging Project ID
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// GoogleCloudLogging Certificade Path
        /// </summary>
        public string CertificatePath { get; set; }
    }
}
