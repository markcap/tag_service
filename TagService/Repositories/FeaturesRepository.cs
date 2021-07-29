using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using SerilogTimings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TagService.Models;

namespace TagService.Repositories
{
    public class FeaturesRepository : IFeaturesRepository
    {
        private readonly TagDbContext _context;
        private readonly ILogger<FeaturesRepository> _logger;
        private readonly string featureAsset = TagAssetType.Feature.ToString();
        private readonly string categoryContext = TagContext.uexpress_subnavigations.ToString();

        public FeaturesRepository(ILogger<FeaturesRepository> logger, TagDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<CategoryFeaturesDto> GetFeatureIdsByCategoryAsync(string category)
        {
            var categoryLower = category.ToLower();
            var categoryTitle = char.ToUpper(category[0]) + category[1..];
            List<long> featureIds;

            using (Operation.At(LogEventLevel.Debug).Time("GetFeatureIdsByCategoryAsync database query"))
            {
                featureIds = await _context.Taggings
                .Include("Tag")
                .Where(t => t.TaggableType == featureAsset && t.Context == categoryContext &&
                    (t.Tag.TagName == categoryLower || t.Tag.TagName == categoryTitle))
                .Select(t => t.TaggableId)
                .ToListAsync();
            }

            var categoryFeatureIds = new CategoryFeaturesDto
            {
                Category = category,
                FeatureIds = featureIds
            };

            return categoryFeatureIds;
        }

        public async Task<List<CategoryFeaturesDto>> GetAllFeatureIdsByCategoryAsync()
        {
            List<CategoryFeaturesDto> categoryFeatureIds;

            using (Operation.At(LogEventLevel.Debug).Time("GetAllFeatureIdsByCategoryAsync database query"))
            {
                var categoryGroups = await _context.Taggings
                .Include("Tag")
                .Where(t => t.TaggableType == featureAsset && t.Context == categoryContext)
                .ToListAsync();

                categoryFeatureIds = categoryGroups
                    .GroupBy(t => t.Tag.TagName)
                    .Select(cf => new CategoryFeaturesDto
                    {
                        Category = cf.Key.ToLower(),
                        FeatureIds = cf.Select(f => f.TaggableId).ToList()
                    })
                    .ToList();
            }

            return categoryFeatureIds;
        }
    }
}
