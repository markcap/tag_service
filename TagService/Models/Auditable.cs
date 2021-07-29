using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TagService.Models
{
    public abstract class Auditable : ITrackable, IActivatable
    {
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public string CreatedBy { get; set; }

        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }

        [JsonIgnore]
        public string ModifiedBy { get; set; }

        [Required]
        [JsonIgnore]
        public bool? ActiveFlag { get; set; } = true;
    }
}
