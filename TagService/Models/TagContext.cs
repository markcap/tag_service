namespace TagService.Models
{
    public enum TagContext
    {
        // case matters here because PostgreSQL is case-sensitive
        subnavigations,
        alt_spellings,
        genres,
        topics,
        uexpress_subnavigations,
        tags
    }
}
