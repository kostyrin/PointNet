using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Core;
using log4net.Layout;

namespace PointNet.Common.Logging
{
    /*http://stackoverflow.com/questions/17752268/log4net-adonetappender-wont-insert-null-values-into-the-database*/
    // взято с http://stackoverflow.com/questions/11691583/autofac-register-component-and-resolve-depending-on-resolving-parent
    public class RawExceptionLayout : IRawLayout
    {
        public virtual object Format(LoggingEvent loggingEvent)
        {
            if (loggingEvent.ExceptionObject != null)
                return loggingEvent.ExceptionObject.ToString();
            else
                return null;
        }
    }

    public class Log4NetAdapter : ILogger
    {
        private readonly log4net.ILog _log;

        static Log4NetAdapter()
        {
            log4net.Config.XmlConfigurator.Configure();
            log4net.Util.SystemInfo.NullText = null;
            log4net.Util.SystemInfo.NotAvailableText = null;
        }

        public Log4NetAdapter(Type type)
        {
            _log = LogManager.GetLogger(type);
        }

        public void Debug(string format, params object[] args)
        {
            _log.DebugFormat(format, args);
        }

        public void Info(string format, params object[] args)
        {
            _log.InfoFormat(format, args);
        }

        public void Warn(string format, params object[] args)
        {
            _log.WarnFormat(format, args);
        }

        public void Error(string format, params object[] args)
        {
            _log.ErrorFormat(format, args);
        }

        public void Error(Exception ex)
        {
            _log.Error("", ex);
        }

        public void Error(Exception ex, string format, params object[] args)
        {
            _log.Error(string.Format(format, args), ex);
        }

        public void Fatal(Exception ex, string format, params object[] args)
        {
            _log.Fatal(string.Format(format, args), ex);
        }
    }
}
