using Flubar.Configuration;
using Flubar.Infrastructure;

namespace Flubar.Diagnostics
{
    //todo: move to separate namespace
    public class DiagnosticLogger : ILog
    {
        readonly IBehaviorConfiguration behaviorConfiguration;

        public DiagnosticLogger(IBehaviorConfiguration behaviorConfiguration)
        {
            Check.NotNull(behaviorConfiguration, nameof(behaviorConfiguration));
            this.behaviorConfiguration = behaviorConfiguration;
        }

        public void Info(string message)
        {
            Check.NotNull(message, nameof(message));
            behaviorConfiguration.Log(DiagnosticLevel.Info, message);
        }

        public void Info(string format, params object[] args)
        {
            Check.NotNull(format, nameof(format));
            Check.NotEmpty(args, nameof(args));
            behaviorConfiguration.Log(DiagnosticLevel.Info, string.Format(format, args));
        }

        public void Warning(string message)
        {
            Check.NotNull(message, nameof(message));
            behaviorConfiguration.Log(DiagnosticLevel.Warning, message);
        }
    }
}
