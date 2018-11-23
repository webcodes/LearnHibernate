namespace LearnHibernate.Api
{
    using System;
    using System.Collections.Generic;
    using NHibernate;
    using Serilog;
    using Serilog.Events;

    internal class SerilogNHLogger : INHibernateLogger
    {
        private ILogger contextLogger;

        public SerilogNHLogger(ILogger contextLogger)
        {
            this.contextLogger = contextLogger;
        }

        private static readonly Dictionary<NHibernateLogLevel, LogEventLevel> MapLevels = new Dictionary<NHibernateLogLevel, LogEventLevel>
        {
            { NHibernateLogLevel.Debug, LogEventLevel.Debug },
            { NHibernateLogLevel.Error, LogEventLevel.Error },
            { NHibernateLogLevel.Fatal, LogEventLevel.Fatal },
            { NHibernateLogLevel.Info, LogEventLevel.Information },
            { NHibernateLogLevel.Trace, LogEventLevel.Verbose },
            { NHibernateLogLevel.Warn, LogEventLevel.Warning }
        };

        public bool IsEnabled(NHibernateLogLevel logLevel)
        {
            // special case because for Serilog there's no none level
            return logLevel == NHibernateLogLevel.None ||
                this.contextLogger.IsEnabled(MapLevels[logLevel]);
        }

        public void Log(NHibernateLogLevel logLevel, NHibernateLogValues state, Exception exception)
        {
            this.contextLogger.Write(MapLevels[logLevel], exception, state.Format, state.Args);
        }
    }
}