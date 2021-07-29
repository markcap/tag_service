using System.Collections.Generic;

namespace TagService.Models
{
    public class CategoryFeaturesDto
    {
        public string Category { get; set; }

        public List<long> FeatureIds { get; set; }
    }
}
