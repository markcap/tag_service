using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TagService.Models;
using TagService.Repositories;

namespace TagService.Services
{
    public class TagsService : ITagsService
    {
        private readonly ITagsRepository _tagsRepository;
        private readonly ILogger<TagsService> _logger;

        public TagsService(ITagsRepository tagsRepository)
        {
            _tagsRepository = tagsRepository;
        }

        public TagsService(ILogger<TagsService> logger, ITagsRepository tagsRepository) : this(tagsRepository)
        {
            _logger = logger;
        }

        public async Task<TagListDto> GetByForeignIdAsync(long foreignId,
                                                       TagAssetType tagAssetType,
                                                       TagContext tagContext)
        {
            return await _tagsRepository.GetByForeignIdAsync(foreignId, tagAssetType, tagContext);
        }

        public async Task<List<TagListDto>> GetByForeignIdsAsync(List<long> foreignIds,
                                                              TagAssetType tagAssetType,
                                                              TagContext tagContext)
        {
            return await _tagsRepository.GetByForeignIdsAsync(foreignIds, tagAssetType, tagContext);
        }
    }
}
