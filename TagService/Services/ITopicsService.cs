using System.Collections.Generic;
using System.Threading.Tasks;
using TagService.Models;

namespace TagService.Services
{
    public interface ITopicsService
    {
        public Task<TagListDto> GetByIdAsync(long assetId, TagAssetType assetType);

        public Task<List<TagListDto>> GetByIdsAsync(List<long> assetIds, TagAssetType assetType);

        public Task<List<long>> GetByFeatureTopic(long featureId, string topic, int count, int skipCount, TagAssetType assetType);
    }
}
