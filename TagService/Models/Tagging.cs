using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TagService.Models
{
    public class Tagging : Auditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TaggingId { get; set; }

        [Required]
        public string TaggableType { get; set; } // feature, asset, publication

        [Required]
        public long TaggableId { get; set; } // featureId

        public long? TaggerId { get; set; }

        public long? FeatureId { get; set; }

        public string TaggerType { get; set; } // user, externalUser

        [Required]
        public string Context { get; set; } // topics, tags

        [Required]
        public Tag Tag { get; set; }
    }
}
