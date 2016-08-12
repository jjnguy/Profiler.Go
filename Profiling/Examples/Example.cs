using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nest;
using Profiling;
using Profiling.Handlers;

namespace Examples
{
    [TestClass]
    public class Example
    {
        [TestMethod]
        public void Example1()
        {
            var profiler = new Profiler(new ConsoleProfileResultHandler(), "example", "simple");
            profiler.Go("Counting from 1 to a million", () =>
            {
                var count = 0;
                for (int i = 0; i < 1000000; i++)
                {
                    count = i;
                }
            });

            var sqrt = profiler.Go("Calculating square root of 2", () => Math.Sqrt(2));
        }

        [TestMethod]
        public void Example2()
        {
            var profiler = new Profiler(new ElasticSearchProfileResultHandler(new ElasticClient(new Uri("example ES uri")), "my-profiling-index"), "example", "Elasticsearch");
            profiler.Go("Counting from 1 to a million", () =>
            {
                var count = 0;
                for (int i = 0; i < 1000000; i++)
                {
                    count = i;
                }
            });
        }
    }
}
