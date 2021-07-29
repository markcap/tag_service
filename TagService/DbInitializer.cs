namespace TagService
{
    using Bogus;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TagService.Models;

    public static class DbInitializer
    {
        public static async Task SeedDb(this IApplicationBuilder app, bool development = false)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<TagDbContext>();
            var tagCount = await context.Tags.CountAsync();

            if (development && tagCount == 0)
            {
                context.Seed();
            }
        }

        private static void Seed(this TagDbContext context)
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
                .RuleFor(t => t.TaggableType, "Feature")
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
