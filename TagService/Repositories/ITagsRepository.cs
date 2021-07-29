using System.Collections.Generic;
using System.Threading.Tasks;
using TagService.Models;

namespace TagService.Repositories
{
    public interface ITagsRepository
    {
        public Task<TagListDto> GetByForeignIdAsync(long foreignId, TagAssetType tagAssetType, TagContext tagContext);

        public Task<List<TagListDto>> GetByForeignIdsAsync(List<long> foreignId, TagAssetType tagAssetType, TagContext tagContext);

        public Task<List<string>> GetAllCategoriesAsync();

        public Task<List<long>> GetByFeatureTopicAsync(long featureId, string topic, int count, int skipCount, TagAssetType assetType, TagContext tagContext);
    }
}
