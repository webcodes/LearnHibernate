namespace LearnHibernate.Api
{
    using System;
    using NHibernate;
    using Serilog;
    using Serilog.Core;

    internal class SerilogNHLoggerFactory : INHibernateLoggerFactory
    {
        public INHibernateLogger LoggerFor(string keyName)
        {
            var contextLogger = Log.Logger.ForContext(Constants.SourceContextPropertyName, keyName);
            return new SerilogNHLogger(contextLogger);
        }

        public INHibernateLogger LoggerFor(Type type)
        {
            var contextLogger = Log.Logger.ForContext(type);
            return new SerilogNHLogger(contextLogger);
        }
    }
}