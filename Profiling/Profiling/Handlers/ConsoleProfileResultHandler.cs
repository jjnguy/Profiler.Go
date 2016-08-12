using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiling.Handlers
{
    public class ConsoleProfileResultHandler : IProfileResultHandler
    {
        public void LogProfileResults(Guid session, string label, TimeSpan elapsed, string[] tags)
        {
            Console.WriteLine(
                $"{session}: {elapsed.TotalMilliseconds,8} milis performing {label} [{tags.Aggregate("", (t, t2) => t + ", " + t2, result => result.Substring(2))}]");
        }
    }
}
