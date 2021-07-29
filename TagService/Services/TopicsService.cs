using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TagService.Models;
using TagService.Repositories;

namespace TagService.Services
{
    public class TopicsService : ITopicsService
    {
        private readonly ITagsRepository _tagsRepository;
        private readonly ILogger<TopicsService> _logger;

        public TopicsService(ITagsRepository tagsRepository)
        {
            _tagsRepository = tagsRepository;
        }

        public TopicsService(ILogger<TopicsService> logger, ITagsRepository tagsRepository) : this(tagsRepository)
        {
            _logger = logger;
        }

        public async Task<TagListDto> GetByIdAsync(long foreignId, TagAssetType assetType)
        {
            var topicList = await _tagsRepository.GetByForeignIdAsync(foreignId,
                                                                      assetType,
                                                                      TagContext.topics);
            return topicList;
        }

        public async Task<List<TagListDto>> GetByIdsAsync(List<long> foreignId, TagAssetType assetType)
        {
            var topicLists = await _tagsRepository.GetByForeignIdsAsync(foreignId,
                                                                        assetType,
                                                                        TagContext.topics);
            return topicLists;
        }

        public async Task<List<long>> GetByFeatureTopic(long featureId, string topic, int count, int skipCount, TagAssetType assetType)
        {
            var assetIds = await _tagsRepository.GetByFeatureTopicAsync(featureId,
                                                                        topic,
                                                                        count,
                                                                        skipCount,
                                                                        assetType,
                                                                        TagContext.topics);
            return assetIds;
        }
    }
}
