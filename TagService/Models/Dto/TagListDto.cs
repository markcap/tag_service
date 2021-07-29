using System.Collections.Generic;

namespace TagService.Models
{
    public class TagListDto
    {
        public long ForeignId { get; set; }

        public string TagType { get; set; }

        public string TagContext { get; set; }

        public List<TagsWithSlugsDto> TagNames { get; set; }
    }
}
