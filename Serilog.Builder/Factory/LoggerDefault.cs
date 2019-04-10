using System;
using System.Threading.Tasks;

namespace Serilog.Builder.Factory
{
    /// <summary>
    /// Serilog Logger default class
    /// </summary>
    public class LoggerDefault : ILoggerDefault
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="logger"></param>
        public LoggerDefault(ILogger logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// Log async as debug
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        public async Task DebugAsync(string message)
        {
            await Task.Run(() => _logger.Debug(message)).ConfigureAwait(false);
        }

        /// <summary>
        /// Log async with information level
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        public async Task InfoAsync(string message)
        {
            await Task.Run(() => _logger.Information(message)).ConfigureAwait(false);
        }

        /// <summary>
        /// Log async with warning level
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        public async Task WarningAsync(string message)
        {
            await Task.Run(() => _logger.Warning(message)).ConfigureAwait(false);
        }

        /// <summary>
        /// Log async with warning level with exception
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="ex">Exception</param>
        /// <returns></returns>
        public async Task WarningAsync(string message, Exception ex)
        {
            await Task.Run(() => _logger.Warning(ex, message)).ConfigureAwait(false);
        }

        /// <summary>
        /// Log async with error level
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        public async Task ErrorAsync(string message)
        {
            await Task.Run(() => _logger.Error(message)).ConfigureAwait(false);
        }

        /// <summary>
        /// Log async with error level with exception
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="ex">Exception</param>
        /// <returns></returns>
        public async Task ErrorAsync(string message, Exception ex)
        {
            await Task.Run(() => _logger.Error(ex, message)).ConfigureAwait(false);
        }
    }
}