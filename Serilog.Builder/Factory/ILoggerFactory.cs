namespace Serilog.Builder.Factory
{
    /// <summary>
    /// Serilog Logger factory interface
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Create logger default
        /// </summary>
        /// <returns></returns>
        ILoggerDefault Create();
    }
}
