using System.Collections.Generic;
using System.Threading.Tasks;
using TagService.Models;

namespace TagService.Services
{
    public interface ITagsService
    {
        public Task<TagListDto> GetByForeignIdAsync(long foreignId, TagAssetType tagAssetType, TagContext tagContext);

        public Task<List<TagListDto>> GetByForeignIdsAsync(List<long> foreignId, TagAssetType tagAssetType, TagContext tagContext);
    }
}
