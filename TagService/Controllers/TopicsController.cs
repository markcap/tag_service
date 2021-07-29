using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TagService.Models;
using TagService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TagService.Controllers
{
    [Route("v{version:apiVersion}/topics")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TopicsController : ControllerBase
    {
        private readonly ILogger<TopicsController> _logger;
        private readonly ITopicsService _topicsService;

        public TopicsController(ILogger<TopicsController> logger, ITopicsService topicsService)
        {
            _logger = logger;
            _topicsService = topicsService;
        }
        
        [HttpGet("assetId/{assetId}")]
        public async Task<List<TagsWithSlugsDto>> GetByAssetId(long assetId)
        {
            var tagList = await _topicsService.GetByIdAsync(assetId, TagAssetType.Asset);
            
            return tagList.TagNames;
        }

        [HttpGet("assetIds/")]
        public async Task<List<TagListDto>> GetByAssetIds([FromQuery, BindRequired] List<long> assetIds)
        {
            return await _topicsService.GetByIdsAsync(assetIds, TagAssetType.Asset);
        }

        [HttpGet("featureId/{featureId}")]
        public async Task<List<TagsWithSlugsDto>> GetByFeatureId(long featureId)
        {
            var tagList = await _topicsService.GetByIdAsync(featureId, TagAssetType.Feature);

            return tagList.TagNames;
        }

        // count is the value that determines how many articles the front-end wants returned for that given call.
        // skipCount is the value of articles that is already being shown on the page, so that the Tags DB which has no concept of publish date can keep track of where the last query ended.
        [HttpGet("featureTopic/featureId/{featureId}")]
        public async Task<List<long>> GetByFeatureTopic(long featureId, string topic, int count, int skipCount)
        {
            var assetIds = await _topicsService.GetByFeatureTopic(featureId, topic, count, skipCount, TagAssetType.Asset);

            return assetIds;
        }
    }
}
