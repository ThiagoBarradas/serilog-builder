using System.Collections.Generic;

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

        /// <summary>
        /// Use json output
        /// </summary>
        public bool UseJsonOutput { get; set; } = true;

        /// <summary>
        /// ResourceType
        /// </summary>
        public string ResourceType { get; internal set; }

        /// <summary>
        /// Labels
        /// </summary>
        public string Labels { get; internal set; }

        /// <summary>
        /// Resource Labels
        /// </summary>
        public Dictionary<string, string> ResourceLabels { get; internal set; }
    }
}
