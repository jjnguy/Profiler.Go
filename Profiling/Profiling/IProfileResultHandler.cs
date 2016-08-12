using System;

namespace Profiling
{
    public interface IProfileResultHandler
    {
        void LogProfileResults(Guid session, string label, TimeSpan elapsed, string[] tags);
        void LogProfileStart(Guid session, string label, DateTime start, string[] tags);
        void LogProfileEnd(Guid session, string label, DateTime end, string[] tags);
    }

    public abstract class BaseProfileResultHandler : IProfileResultHandler
    {
        public abstract void LogProfileResults(Guid session, string label, TimeSpan elapsed, string[] tags);

        public virtual void LogProfileStart(Guid session, string label, DateTime start, string[] tags)
        {
            
        }

        public virtual void LogProfileEnd(Guid session, string label, DateTime end, string[] tags)
        {
            
        }
    }
}
