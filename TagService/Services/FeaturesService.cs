using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TagService.Models;
using TagService.Repositories;

namespace TagService.Services
{
    public class FeaturesService : IFeaturesService
    {
        private readonly IFeaturesRepository _featuressRepository;
        private readonly ILogger<FeaturesService> _logger;

        public FeaturesService(ILogger<FeaturesService> logger, IFeaturesRepository featuressRepository)
        {
            _logger = logger;
            _featuressRepository = featuressRepository;
        }

        public async Task<CategoryFeaturesDto> GetFeatureIdsByCategoryAsync(string category)
        {
            var featureIds = await _featuressRepository.GetFeatureIdsByCategoryAsync(category);
            return featureIds;
        }

        public async Task<List<CategoryFeaturesDto>> GetAllFeatureIdsByCategoryAsync()
        {
            var categoryFeaturesIds = await _featuressRepository.GetAllFeatureIdsByCategoryAsync();
            return categoryFeaturesIds;
        }
    }
}
