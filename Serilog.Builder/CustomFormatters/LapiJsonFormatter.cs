using Serilog.Builder.Models.Mappers;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Serilog.Builder.CustomFormatters
{
    public class LapiJsonFormatter : ITextFormatter
    {
        private const char SEPARATOR = ':';
        private const string DEFAULT_DELIMITER = ",";
        private readonly string _index;
        private static readonly JsonValueFormatter _valueFormatter = new JsonValueFormatter();

        public LapiJsonFormatter(string index)
        {
            if (string.IsNullOrWhiteSpace(index))
                throw new ArgumentNullException(nameof(index));

            _index = index;
        }

        public void Format(LogEvent logEvent, TextWriter output)
        {
            if (logEvent is null)
                throw new ArgumentNullException(nameof(logEvent));

            if (output is null)
                throw new ArgumentNullException(nameof(output));

            var lapiMapper = ArrangeLogData(logEvent, _index);

            WriteAndFormatPropertiesToSplunk(logEvent, lapiMapper, output);
            WriteDefaultSuffixPropertiesToSplunk(lapiMapper, output);
        }

        private static LogLapiMapper ArrangeLogData(LogEvent logEvent, string index)
        {
            var message = logEvent.RenderMessage();

            var lapiMapper = new LogLapiMapper
            {
                Timestamp = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture),
                ProductName = index,
                MachineName = Environment.MachineName,
                Severity = logEvent.Level.ToString().Replace("Information", "Info"),
                Message = message.Replace(@"""", "")
            };

            return lapiMapper;
        }

        private static void WriteAndFormatPropertiesToSplunk(LogEvent logEvent, LogLapiMapper logLapiMapper, TextWriter output)
        {
            output.Write("{\"Timestamp\":\"");
            output.Write(logLapiMapper.Timestamp + "\"");

            var additionalDataToBeRemoved = new string[] { "SplunkIndex", "ProductCompany", "ProductVersion", "ProcessName" };

            var propertiesFiltered = logEvent.Properties.Where(
                prop => !additionalDataToBeRemoved.Contains(prop.Key))
                .ToDictionary(d => d.Key, d => d.Value);

            if (logEvent.Properties.Count != 0)
                WriteEventProperties(propertiesFiltered, logEvent.Exception, output);
        }

        private static void WriteDefaultSuffixPropertiesToSplunk(LogLapiMapper logLapiMapper, TextWriter output)
        {
            WriteSuffixQuotedJsonString(",\"Message\":", logLapiMapper.Message, output);
            WriteSuffixQuotedJsonString(",\"MachineName\":", logLapiMapper.MachineName, output);
            WriteSuffixQuotedJsonString(",\"ProductName\":", logLapiMapper.ProductName, output);
            WriteSuffixQuotedJsonString(",\"Severity\":", logLapiMapper.Severity, output);

            // Close splunk props
            output.Write("}");
        }

        private static void WriteEventProperties(Dictionary<string, LogEventPropertyValue> properties, Exception exception, TextWriter output)
        {
            output.Write(",\"AdditionalData\":{");

            var precedingDelimiter = "";
            foreach (var property in properties)
            {
                output.Write(precedingDelimiter);
                precedingDelimiter = DEFAULT_DELIMITER;
                WriteLogEventProperty(property.Key, property.Value, output);
            }

            if (exception != null)
                WriteException(exception, output);

            output.Write('}');
        }

        private static void WriteException(Exception exception, TextWriter output)
        {
            output.Write(",\"Exception\":{");

            WriteStringProperty("Message", exception.Message, output);

            output.Write(DEFAULT_DELIMITER);

            WriteStringProperty("StackTrace", exception.StackTrace ?? string.Empty, output);

            output.Write('}');
        }

        private static void WriteLogEventProperty(string nameProperty, LogEventPropertyValue log, TextWriter output)
        {
            WriteNameProperty(nameProperty, output);
            _valueFormatter.Format(log, output);
        }

        private static void WriteStringProperty(string nameProperty, string str, TextWriter output)
        {
            WriteNameProperty(nameProperty, output);
            JsonValueFormatter.WriteQuotedJsonString(str, output);
        }

        private static void WriteNameProperty(string nameProperty, TextWriter output)
        {
            JsonValueFormatter.WriteQuotedJsonString(nameProperty, output);
            output.Write(SEPARATOR);
        }

        private static void WriteSuffixQuotedJsonString(string propertyName, string str, TextWriter output)
        {
            if (!string.IsNullOrWhiteSpace(propertyName) && !string.IsNullOrWhiteSpace(str))
            {
                output.Write(propertyName);
                JsonValueFormatter.WriteQuotedJsonString(str, output);
            }
        }
    }
}
