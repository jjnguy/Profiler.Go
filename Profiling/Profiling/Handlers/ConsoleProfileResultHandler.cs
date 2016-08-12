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
                $"{session}: {elapsed.TotalMilliseconds,8} millis performing {label} {BuildTagsString(tags)}");
        }

        public void LogProfileStart(Guid session, string label, DateTime start, string[] tags)
        {
            Console.WriteLine(
                $"{session}: Begin {start.ToString("o")} {label} {BuildTagsString(tags)}");
        }

        public void LogProfileEnd(Guid session, string label, DateTime end, string[] tags)
        {
            Console.WriteLine(
                $"{session}: End   {end.ToString("o")} {label} {BuildTagsString(tags)}");
        }

        private static string BuildTagsString(string[] tags)
        {
            return $"[{tags.Aggregate("", (t, t2) => t + ", " + t2, result => result.Substring(2))}]";
        }
    }
}
