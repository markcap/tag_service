using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using SerilogTimings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TagService.Models;

namespace TagService.Repositories
{
    public class TagsRepository : ITagsRepository
    {
        private readonly TagDbContext _context;
        private readonly ILogger<TagsRepository> _logger;

        public TagsRepository(ILogger<TagsRepository> logger, TagDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<TagListDto> GetByForeignIdAsync(long foreignId,
                                                       TagAssetType tagAssetType,
                                                       TagContext tagContext)
        {
            var assetType = tagAssetType.ToString();
            var context = tagContext.ToString();
            List<TagsWithSlugsDto> tags;

            using (Operation.At(LogEventLevel.Debug).Time("GetByForeignIdAsync database query"))
            {
                tags = await _context.Taggings
                .Include("Tag")
                .Where(t => t.TaggableId == foreignId
                        && t.TaggableType == assetType
                        && t.Context == context)
                .Select(t => new TagsWithSlugsDto
                {   Name = t.Tag.TagName,
                    Slug = t.Tag.Slug
                })
                .ToListAsync();
            }

            var topicList = new TagListDto
            {
                TagType = assetType,
                TagContext = context,
                ForeignId = foreignId,
                TagNames = tags
            };

            return topicList;
        }

        public async Task<List<TagListDto>> GetByForeignIdsAsync(List<long> foreignIds,
                                                              TagAssetType tagAssetType,
                                                              TagContext tagContext)
        {
            var assetType = tagAssetType.ToString();
            var context = tagContext.ToString();
            List<TagListDto> lists;

            using (Operation.At(LogEventLevel.Debug).Time("GetByForeignIdsAsync database query"))
            {
                lists = (await _context.Taggings
                .Include("Tag")
                .Where(t => foreignIds.Contains(t.TaggableId)
                        && t.TaggableType == assetType
                        && t.Context == context)
                .ToListAsync())
                // client side - ef 
                .GroupBy(a => a.TaggableId)
                .Select(t => new TagListDto
                {
                    TagType = assetType,
                    TagContext = context,
                    ForeignId = t.Key,
                    TagNames = t.Select(t2 => new TagsWithSlugsDto
                    {   Name = t2.Tag.TagName,
                        Slug = t2.Tag.Slug
                    }).ToList()
                })
                .ToList();
            }

            return lists;
        }

        public async Task<List<string>> GetAllCategoriesAsync()
        {
            var assetType = TagAssetType.Feature.ToString();
            var context = TagContext.uexpress_subnavigations.ToString();
            List<string> tags;

            using (Operation.At(LogEventLevel.Debug).Time("GetAllCategoriesAsync database query"))
            {
                tags = await _context.Taggings
                .Include("Tag")
                .Where(t => t.TaggableType == assetType && t.Context == context)
                .Select(t => new string(t.Tag.TagName).ToLower())
                .Distinct()
                .ToListAsync();
            }

            return tags;
        }

        public async Task<List<long>> GetByFeatureTopicAsync(long featureId, string topic, int count, int skipCount, TagAssetType assetType, TagContext tagContext)
        {
            var tagAssetType = assetType.ToString();
            var context = tagContext.ToString();
            List<long> list;
            
            using (Operation.At(LogEventLevel.Debug).Time("GetByFeatureTopicAsync database query"))
            {
                list = (await _context.Taggings
                .Include("Tag")
                .Where(t => t.FeatureId == featureId
                        && t.TaggableType == tagAssetType
                        && t.Context == context
                        && t.Tag.TagName == topic)
                .OrderByDescending(t => t.TaggingId)
                .Select(t => t.TaggableId)
                .Take(count + skipCount + 10)
                .ToListAsync());
            }

            list.RemoveRange(0, skipCount);
            return list;
        }
    }
}
