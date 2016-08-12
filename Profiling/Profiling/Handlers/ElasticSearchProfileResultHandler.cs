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
                SessionId = session,
            }, ix => ix.Index(_indexName));
        }

        public void LogProfileStart(Guid session, string label, DateTime start, string[] tags)
        {
            LogProfileStartEnd(session, label, start, tags, "start");
        }

        public void LogProfileEnd(Guid session, string label, DateTime end, string[] tags)
        {
            LogProfileStartEnd(session, label, end, tags, "end");
        }

        private void LogProfileStartEnd(Guid session, string label, DateTime time, string[] tags, string milestoneString)
        {
            _elasticClient.Index(new ElasticProfilingStartEnd
            {
                Label = label,
                Milestone = milestoneString,
                Tags = tags,
                Timestamp = time,
                SessionId = session,
            }, ix => ix.Index(_indexName));
        }
    }

    [ElasticsearchType]
    public abstract class BaseElasticProfilingObj
    {
        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string[] Tags { get; set; }
        public string Label { get; set; }
        [String(Index = FieldIndexOption.NotAnalyzed)]
        public Guid SessionId { get; set; }
    }

    [ElasticsearchType]
    public class ElasticProfilingResult : BaseElasticProfilingObj
    {
        public double MilisElapsed { get; set; }
    }

    [ElasticsearchType]
    public class ElasticProfilingStartEnd : BaseElasticProfilingObj
    {
        public DateTime Timestamp { get; set; }
        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string Milestone { get; set; }
    }
}
