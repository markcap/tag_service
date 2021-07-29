using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagService.Exceptions;
using TagService.Models;
using TagService.Repositories;
using TagService.Services;
using Xunit;
using Xunit.Abstractions;

namespace TagService.UnitTests.Services
{
    public class TopicsServiceUnitTests
    {
        private readonly ITestOutputHelper _output;
        private readonly TopicsService _topicsService;
        private readonly TagsRepository _tagsRepository;
        readonly TagDbContext _dbContext;


        public TopicsServiceUnitTests(ITestOutputHelper output)
        {
            _output = output;
            _dbContext = DbContextMocker.GetTagDbContext(nameof(TopicsServiceUnitTests));
            var mockRepositoryLogger = new Mock<ILogger<TagsRepository>>();
            var mockServicesLogger = new Mock<ILogger<TopicsService>>();

            _tagsRepository = new TagsRepository(mockRepositoryLogger.Object, _dbContext);
            _topicsService = new TopicsService(mockServicesLogger.Object, _tagsRepository);
        }

        [Theory]
        [InlineData(TagAssetType.Feature)]
        [InlineData(TagAssetType.Asset)]
        public async Task GetById_ReturnList(TagAssetType tagAssetType)
        {
            // Arrange
            var featureIds = await GetFeatureIds(tagAssetType, TagContext.topics);
            if (featureIds.Count() > 0)
            {
                var featureId = featureIds[new Random().Next(0, featureIds.Count() - 1)];
                string testString = "category-";
                int testLength = testString.Length;

                // Act
                var topics = await _topicsService.GetByIdAsync(featureId, tagAssetType);

                // Assert
                Assert.IsType<TagListDto>(topics);

                Assert.Equal(testString, topics.TagNames[0].Name.Substring(0, testLength));
                Assert.Equal(TagContext.topics.ToString(), topics.TagContext);
            }
            else
                Assert.NotNull(featureIds);

            _dbContext.Dispose();
        }

        [Theory]
        [InlineData(TagAssetType.Feature)]
        [InlineData(TagAssetType.Asset)]
        public async Task GetById_ReturnEmpty(TagAssetType tagAssetType)
        {
            // Arrange
            var featureIds = await GetFeatureIds(tagAssetType, TagContext.topics);
            var featureId = new Random().Next(1, 110);

            while (featureIds.Contains(featureId))
                featureId = new Random().Next(1, 110);

            // Act
            var featureList = await _topicsService.GetByIdAsync(featureId, tagAssetType);

            // Assert
            Assert.IsType<TagListDto>(featureList);
            Assert.Empty(featureList.TagNames);
        }

        [Theory]
        [InlineData(TagAssetType.Asset)]
        [InlineData(TagAssetType.Feature)]
        public async Task GetByIds_ReturnList(TagAssetType tagAssetType)
        {
            // Arrange
            var featureIds = await GetFeatureIds(tagAssetType, TagContext.topics);

            // Act
            var featureList = await _topicsService.GetByIdsAsync(featureIds, tagAssetType);

            // Assert
            Assert.IsType<List<TagListDto>>(featureList);
            Assert.Equal(featureIds.Count(), featureList.Count());
        }

        [Theory]
        [InlineData(TagAssetType.Feature, TagContext.alt_spellings)]
        [InlineData(TagAssetType.Feature, TagContext.genres)]
        [InlineData(TagAssetType.Feature, TagContext.subnavigations)]
        [InlineData(TagAssetType.Feature, TagContext.tags)]
        [InlineData(TagAssetType.Feature, TagContext.topics)]
        [InlineData(TagAssetType.Feature, TagContext.uexpress_subnavigations)]
        [InlineData(TagAssetType.Asset, TagContext.topics)]
        public async Task GetByIds_ReturnEmpty(TagAssetType tagAssetType, TagContext tagContext)
        {
            // Arrange
            var featureIds = await GetFeatureIds(tagAssetType,tagContext);
            var altTagAssetType = tagAssetType == TagAssetType.Feature ? TagAssetType.Asset : TagAssetType.Feature;
            List<TagListDto> featureList;
            
            // Act
            if (featureIds.Count() > 0)
            {
                featureList = await _topicsService.GetByIdsAsync(featureIds, altTagAssetType);
            }
            else
            {
                featureList = await _topicsService.GetByIdsAsync(new long[] { 99 }.ToList<long>(), altTagAssetType);
            }

            // Assert
            Assert.IsType<List<TagListDto>>(featureList);
            Assert.Empty(featureList);
        }

        private async Task<List<long>> GetFeatureIds(TagAssetType tagAssetType = TagAssetType.Feature, TagContext tagContext = TagContext.uexpress_subnavigations)
        {
            List<long> featureList = new List<long>();
            var allItems = Enumerable.Range(1, 110);
            long[] featureIds = new long[allItems.Count()];
            allItems.ToList().ForEach(i => featureIds[i - 1] = i);

            try
            {
                List<TagListDto> myList = await _tagsRepository.GetByForeignIdsAsync(featureIds.ToList<long>(), tagAssetType, tagContext);
                myList.ForEach(list => featureList.Add(list.ForeignId));
            }
            catch(UexpressNotFoundException e)
            {
                featureList = new List<long>();
            }
            return featureList;
        }

    }
}
