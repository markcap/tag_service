using Bogus;
using System.Collections.Generic;
using System.Linq;
using TagService.Models;

namespace TagService.UnitTests
{
    public static class DbContextExtensions
    {
        public static Faker<Tag> TagFaker;
        public static Faker<Tagging> TaggingFaker;

        public static List<string> _taggableType = new List<string>
        {
            "feature",
            "asset"
        };

        public static void Seed(this TagDbContext context)
        {
            {
                long featureId = 1;
                long tagId = 7;
                long taggingId = 1;

                // Samples for topic (i.e., category) taggings
                List<Tag> categoryTags = new List<Tag>() {
                new Tag() { TagId = 1, TagName = "category-advice" },
                new Tag() { TagId = 2, TagName = "category-pets" },
                new Tag() { TagId = 3, TagName = "category-health" },
                new Tag() { TagId = 4, TagName = "category-parenting" },
                new Tag() { TagId = 5, TagName = "category-humor" },
                new Tag() { TagId = 6, TagName = "category-deleted", ActiveFlag = false }
            };
                context.Tags.AddRange(categoryTags);

                // Sample for tag taggings
                var tagFaker = new Faker<Tag>()
                    .RuleFor(t => t.TagId, f => tagId++)
                    .RuleFor(t => t.TagName, f => "tag-" + f.Lorem.Word());

                List<Tag> otherTags = tagFaker.Generate(50);
                // Filter out any duplicates
                otherTags = otherTags.GroupBy(x => x.TagName).Select(y => y.First()).ToList();
                context.Tags.AddRange(otherTags);

                var categoryTaggingFaker = new Faker<Tagging>()
                    .RuleFor(t => t.TaggingId, f => taggingId++)
                    .RuleFor(t => t.TaggableType, f => f.PickRandomParam(new string[4] { "Asset", "Feature", "Feature", "Feature" }))
                    .RuleFor(t => t.TaggableId, f => featureId++)
                    .RuleFor(t => t.Context, "topics")
                    .RuleFor(t => t.Tag, f => f.PickRandom(categoryTags));

                List<Tagging> featureTaggings = categoryTaggingFaker.Generate(20);
                context.Taggings.AddRange(featureTaggings);

                var tagTaggingFaker = new Faker<Tagging>()
                    .RuleFor(t => t.TaggingId, f => taggingId++)
                    .RuleFor(t => t.TaggableType, f => f.PickRandomParam(new string[4] { "Asset", "Asset", "Asset", "Feature" }))
                    .RuleFor(t => t.TaggableId, f => featureId++)
                    .RuleFor(t => t.Context, "tags")
                    .RuleFor(t => t.Tag, f => f.PickRandom(otherTags));

                List<Tagging> otherTagging = tagTaggingFaker.Generate(60);
                context.Taggings.AddRange(otherTagging);

                var anotherCategoryTaggingFaker = new Faker<Tagging>()
                    .RuleFor(t => t.TaggingId, f => taggingId++)
                    .RuleFor(t => t.TaggableType, f => f.PickRandomParam(new string[4] { "Asset", "Asset", "Asset", "Feature" }))
                    .RuleFor(t => t.TaggableId, f => featureId++)
                    .RuleFor(t => t.Context, "uexpress_subnavigations")
                    .RuleFor(t => t.Tag, f => f.PickRandom(categoryTags));

                List<Tagging> anotherTagging = anotherCategoryTaggingFaker.Generate(30);
                context.Taggings.AddRange(anotherTagging);

                try
                {
                    context.SaveChanges();
                }
                catch (System.Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
