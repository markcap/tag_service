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
using Xunit;
using Xunit.Abstractions;

namespace TagService.UnitTests.Repository
{
   public class TagsRepositoryUnitTests
    {
        private readonly ITestOutputHelper _output;
        private readonly TagsRepository _tagsRepository;
        readonly TagDbContext _dbContext;

        public TagsRepositoryUnitTests(ITestOutputHelper output)
        {
            _output = output;
            _dbContext = DbContextMocker.GetTagDbContext(nameof(TagsRepositoryUnitTests));
            var mockTagsLogger = new Mock<ILogger<TagsRepository>>();
            _tagsRepository = new TagsRepository(mockTagsLogger.Object, _dbContext);
        }

        [Fact]
        public async Task GetByForeignId_ReturnTopic()
        {
            // Arrange
            var foreignId = 1;
            TagAssetType tagAssetType = TagAssetType.Feature;
            TagContext tagContext = TagContext.topics;

            // Act
            var tagList = await _tagsRepository.GetByForeignIdAsync(foreignId, tagAssetType, tagContext);

            // Assert
            Assert.IsType<TagListDto>(tagList);

            Assert.Equal(tagAssetType.ToString(), tagList.TagType);
            Assert.Equal(tagContext.ToString(), tagList.TagContext);
            Assert.Equal(foreignId, tagList.ForeignId);

            _dbContext.Dispose();
        }

        [Theory]
        [InlineData(1, TagAssetType.Feature,TagContext.alt_spellings)]
        [InlineData(1, TagAssetType.Feature, TagContext.genres)]
        [InlineData(1, TagAssetType.Feature, TagContext.subnavigations)]
        [InlineData(1, TagAssetType.Feature, TagContext.tags)]
        [InlineData(1, TagAssetType.Feature, TagContext.uexpress_subnavigations)]
        [InlineData(1, TagAssetType.Asset, TagContext.alt_spellings)]
        [InlineData(99, TagAssetType.Feature, TagContext.topics)]
        public async Task GetByForeignId_ReturnEmpty(long featureId, TagAssetType tagAssetType, TagContext tagContext)
        {
            // Arrange

            // Act
            var featureList = await _tagsRepository.GetByForeignIdAsync(featureId, tagAssetType, tagContext);

            // Assert
            Assert.IsType<TagListDto>(featureList);
            Assert.Empty(featureList.TagNames);
        }

        [Theory]
        [InlineData(TagAssetType.Feature, TagContext.topics)]
        [InlineData(TagAssetType.Feature, TagContext.uexpress_subnavigations)]
        public async Task GetByForeignIds_ReturnList(TagAssetType tagAssetType, TagContext tagContext)
        {
            // Arrange
            List<long> foreignId = await GetFeatureIds(tagAssetType, tagContext);
           

            // Act
            var tagList = await _tagsRepository.GetByForeignIdsAsync(foreignId.ToList<long>(), tagAssetType, tagContext);

            // Assert
            Assert.IsType<List<TagListDto>>(tagList);

            TagListDto thisTag = ((IEnumerable<TagListDto>)tagList).ToList<TagListDto>()[0];

            Assert.Equal(tagAssetType.ToString(), thisTag.TagType);
            Assert.Equal(tagContext.ToString(), thisTag.TagContext);
            Assert.Equal(foreignId[0], thisTag.ForeignId);

            _dbContext.Dispose();
        }

        [Theory]
        [InlineData(new object[] { new long[] { 99 }, TagAssetType.Feature, TagContext.topics })]
        [InlineData(new object[] { new long[] { 99 }, TagAssetType.Asset, TagContext.topics })]
        [InlineData(new object[] { new long[] { 1 }, TagAssetType.Feature, TagContext.alt_spellings })]
        [InlineData(new object[] { new long[] { 1 }, TagAssetType.Feature, TagContext.genres})]
        [InlineData(new object[] { new long[] { 1 }, TagAssetType.Feature, TagContext.subnavigations})]
        [InlineData(new object[] { new long[] { 1 }, TagAssetType.Feature, TagContext.tags })]
        [InlineData(new object[] { new long[] { 1 }, TagAssetType.Feature, TagContext.uexpress_subnavigations })]
        public async Task GetByForeignIds_ReturnEmpty(long[] idArray, TagAssetType tagAssetType, TagContext tagContext)
        {
            // Arrange
            List<long> foreignIds = idArray.ToList<long>();

            // Act
            var foreignList = await _tagsRepository.GetByForeignIdsAsync(foreignIds, tagAssetType, tagContext);

            // Assert
            Assert.IsType<List<TagListDto>>(foreignList);
            Assert.Empty(foreignList);
        }


        /*************************
         * Private method - Change to Public to test
         * ************************/
        //[Fact]
        //public async Task GetAllAsync_ReturnList()
        //{
        //    // Arrange
        //    var assetType = TagAssetType.Feature;
        //    var context = TagContext.topics;

        //    // Act
        //    var tagList = await _tagsRepository.GetAllAsync(assetType, context);

        //    // Assert
        //    Assert.IsType<List<string>>(tagList);

        //    List<string> thisTag = ((IEnumerable<string>)tagList).ToList<string>();
        //    Assert.Equal("category-", thisTag[0].Substring(0,9));

        //    _dbContext.Dispose();
        //}

        //[Theory]
        //[InlineData(null, null)]
        //[InlineData(TagAssetType.Feature, null )]
        //[InlineData(TagAssetType.Feature, TagContext.alt_spellings )]
        //[InlineData(null, TagContext.topics)]
        //public async Task GetAllAsync_NotFound(TagAssetType? tagAssetType, TagContext? tagContext)
        //{
        //    // Arrange

        //    // Act and Assert
        //    await Assert.ThrowsAsync<UexpressNotFoundException>(() => _tagsRepository.GetAllAsync(tagAssetType, tagContext));

        //    _dbContext.Dispose();
        //}

        [Fact]
        public async Task GetAllAsync_ReturnList()
        {
            // Arrange

            // Act
            var tagList = await _tagsRepository.GetAllCategoriesAsync();

            // Assert
            Assert.IsType<List<string>>(tagList);

            List<string> thisTag = ((IEnumerable<string>)tagList).ToList<string>();
            Assert.Equal("category-", thisTag[0].Substring(0, 9));

            _dbContext.Dispose();
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
            catch (UexpressNotFoundException e)
            {
                featureList = new List<long>();
            }
            return featureList;
        }

    }
}
