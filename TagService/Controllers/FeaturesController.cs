using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TagService.Models;
using TagService.Services;

namespace TagService.Controllers
{
    [Route("v{version:apiVersion}/features")]
    [ApiController]
    [ApiVersion("1.0")]
    public class FeaturesController : ControllerBase
    {

        private readonly ILogger<FeaturesController> _logger;
        private readonly IFeaturesService _featuresService;

        public FeaturesController(ILogger<FeaturesController> logger, IFeaturesService featuresService)
        {
            _logger = logger;
            _featuresService = featuresService;
        }

        [HttpGet("category/{category}")]
        public async Task<CategoryFeaturesDto> GetByCategory(string category)
        {
            var categoryFeatures = await _featuresService.GetFeatureIdsByCategoryAsync(category);
            return categoryFeatures;
        }

        [HttpGet("by-categories")]
        public async Task<List<CategoryFeaturesDto>> GetAllByCategories()
        {
            var categoryFeatures = await _featuresService.GetAllFeatureIdsByCategoryAsync();
            return categoryFeatures;
        }
    }
}
