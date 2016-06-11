namespace Flubar.Diagnostics
{
    public interface ILog
    {
        void Info(string message);
        void Info(string format, params object[] args);
        void Warning(string message);
    }
}
