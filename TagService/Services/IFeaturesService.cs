using System.Collections.Generic;
using System.Threading.Tasks;
using TagService.Models;

namespace TagService.Services
{
    public interface IFeaturesService
    {
        Task<CategoryFeaturesDto> GetFeatureIdsByCategoryAsync(string category);

        Task<List<CategoryFeaturesDto>> GetAllFeatureIdsByCategoryAsync();
    }
}
