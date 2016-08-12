using System;

namespace Profiling
{
    public interface IProfileResultHandler
    {
        void LogProfileResults(Guid session, string label, TimeSpan elapsed, string[] tags);
    }
}
