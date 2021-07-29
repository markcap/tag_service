using System.Collections.Generic;
using System.Threading.Tasks;
using TagService.Models;

namespace TagService.Repositories
{
    public interface IFeaturesRepository
    {
        Task<CategoryFeaturesDto> GetFeatureIdsByCategoryAsync(string category);

        Task<List<CategoryFeaturesDto>> GetAllFeatureIdsByCategoryAsync();
    }
}
