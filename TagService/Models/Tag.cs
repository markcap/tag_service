using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TagService.Models
{
    public class Tag : Auditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TagId { get; set; }

        [Required]
        public string TagName { get; set; }

        public string Slug { get; set; }

        [Required]
        public List<Tagging> Taggings { get; set; }
    }
}
