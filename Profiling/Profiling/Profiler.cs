using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiling
{
    public class Profiler
    {
        private readonly IProfileResultHandler _resultHandler;
        private readonly string[] _tags;
        private readonly Stopwatch _watch;
        private Guid _session = Guid.NewGuid();
        public Profiler(IProfileResultHandler resultHandler, params string[] tags)
        {
            _resultHandler = resultHandler;
            _tags = tags;
            _watch = new Stopwatch();
        }

        public void Go(string label, Action code)
        {
            var result = Go(label, () =>
            {
                code();
                return false;
            });
        }

        public T Go<T>(string label, Func<T> code)
        {
            _resultHandler.LogProfileStart(_session, label, DateTime.UtcNow, _tags);

            _watch.Start();
            var result = code();
            _watch.Stop();

            _resultHandler.LogProfileEnd(_session, label, DateTime.UtcNow, _tags);
            _resultHandler.LogProfileResults(_session, label, _watch.Elapsed, _tags);

            _watch.Reset();
            return result;
        }
    }
}
