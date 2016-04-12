namespace Flubar
{
    class DiagnosticLogger : ILog
    {
        readonly IBehaviorConfiguration behaviorConfiguration;

        public DiagnosticLogger(IBehaviorConfiguration behaviorConfiguration)
        {
            this.behaviorConfiguration = behaviorConfiguration;
        }

        public void Info(string message)
        {
            behaviorConfiguration.Log(DiagnosticLevel.Info, message);
        }

        public void Info(string format, params object[] args)
        {
            behaviorConfiguration.Log(DiagnosticLevel.Info, string.Format(format, args));
        }

        public void Warning(string message)
        {
            behaviorConfiguration.Log(DiagnosticLevel.Warning, message);
        }
    }
}
