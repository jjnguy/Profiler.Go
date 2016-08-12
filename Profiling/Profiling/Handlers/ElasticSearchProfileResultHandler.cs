using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Profiling.Handlers
{
    public class ElasticSearchProfileResultHandler : IProfileResultHandler
    {
        private readonly ElasticClient _elasticClient;
        private readonly string _indexName;

        public ElasticSearchProfileResultHandler(ElasticClient elasticClient, string indexName)
        {
            _elasticClient = elasticClient;
            _indexName = indexName;
            if (!_elasticClient.IndexExists(indexName).Exists)
            {
                _elasticClient.CreateIndex(indexName, ix => ix
                    .Mappings(m => m.Map<ElasticProfilingResult>(mm => mm.AutoMap()))
                );
            }
        }

        public void LogProfileResults(Guid session, string label, TimeSpan elapsed, string[] tags)
        {
            _elasticClient.Index(new ElasticProfilingResult
            {
                Label = label,
                MilisElapsed = elapsed.TotalMilliseconds,
                Tags = tags,
            }, ix => ix.Index(_indexName));
        }
    }

    [ElasticsearchType]
    public class ElasticProfilingResult
    {
        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string[] Tags { get; set; }
        public string Label { get; set; }

        public double MilisElapsed { get; set; }
    }
}
