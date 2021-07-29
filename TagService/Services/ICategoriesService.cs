using System.Collections.Generic;
using System.Threading.Tasks;

namespace TagService.Services
{
    public interface ICategoriesService
    {
        public Task<IEnumerable<string>> GetAllAsync();

        public Task<string> GetByFeatureIdAsync(long featureId);
    }
}
