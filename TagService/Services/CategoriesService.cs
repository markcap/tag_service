using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TagService.Exceptions;
using TagService.Models;
using TagService.Repositories;

namespace TagService.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ITagsRepository _tagsRepository;
        private readonly ILogger<TagsRepository> _logger;


        public CategoriesService(ITagsRepository tagsRepository)
        {
            _tagsRepository = tagsRepository;
        }

        public CategoriesService(ILogger<TagsRepository> logger, ITagsRepository tagsRepository) : this(tagsRepository)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<string>> GetAllAsync()
        {
            return await _tagsRepository.GetAllCategoriesAsync();
        }

        public async Task<string> GetByFeatureIdAsync(long featureId)
        {
            var tagList = await _tagsRepository.GetByForeignIdAsync(featureId, 
                                                                    TagAssetType.Feature,
                                                                    TagContext.uexpress_subnavigations);
            if (tagList.TagNames.Count == 0)
            {
                throw new UexpressNotFoundException($"Category not found for featureId {featureId}.");
            }

            string category = tagList.TagNames[0].Name.ToLower();

            return category;
        }
    }
}
