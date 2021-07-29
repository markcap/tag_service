using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TagService.Exceptions;
using TagService.Models;
using TagService.Repositories;
using TagService.Services;
using Xunit;
using Xunit.Abstractions;

namespace TagService.UnitTests.Services
{
    public class CategoriesServiceUnitTests
    {
        private readonly ITestOutputHelper _output;
        private readonly CategoriesService _categoriesService;
        private readonly TagsRepository _tagsRepository;
        readonly TagDbContext _dbContext;

        public CategoriesServiceUnitTests(ITestOutputHelper output)
        {
            _output = output;
            _dbContext = DbContextMocker.GetTagDbContext(nameof(CategoriesServiceUnitTests));
            var mockRepositoryLogger = new Mock<ILogger<TagsRepository>>();
            _tagsRepository = new TagsRepository(mockRepositoryLogger.Object, _dbContext);

            _categoriesService = new CategoriesService(mockRepositoryLogger.Object, _tagsRepository);

        }

        [Fact]
        public async Task GetAllAsync_ReturnsList()
        {
            // Arrange
            string testString = "category-";
            int testLength = testString.Length;

            // Act
            var catList = await _categoriesService.GetAllAsync();

            // Assert
            Assert.IsType<List<string>>(catList);

            string category = catList.ToList<string>()[0];
            Assert.Equal(testString, category.Substring(0, testLength));

            _dbContext.Dispose();
        }

        [Fact]
        public async Task GetByFeatureId_ReturnString()
        {
            // Arrange
            var featureIds = await GetFeatureIds();
            string testString = "category-";
            int testLength = testString.Length;

            // Act 
            var category = await _categoriesService.GetByFeatureIdAsync(featureIds[0]);

            // Assert
            Assert.Equal(testString, category.Substring(0, testLength));

            _dbContext.Dispose();
        }

        [Fact]
        public async Task GetByFeatureId_ThrowException()
        {
            // Arrange
            var featureId = (long)new Random().Next(1, 110);
            var validIds = await GetFeatureIds();

            while (validIds.Contains(featureId))
                featureId= new Random().Next(1, 110);

            // Act and Assert
            await Assert.ThrowsAsync<UexpressNotFoundException>(() => _categoriesService.GetByFeatureIdAsync(featureId));
        }

        private async Task<List<long>> GetFeatureIds(TagAssetType tagAssetType = TagAssetType.Feature, TagContext tagContext = TagContext.uexpress_subnavigations)
        {
            List<long> featureList = new List<long>();
            var allItems = Enumerable.Range(1, 110);
            long[] featureIds = new long[allItems.Count()];
            allItems.ToList().ForEach(i => featureIds[i - 1] = i);

            List<TagListDto> myList = await _tagsRepository.GetByForeignIdsAsync(featureIds.ToList<long>(), tagAssetType, tagContext);
            myList.ForEach(list => featureList.Add(list.ForeignId));

            return featureList;
        }

        /*
        [Fact]
        public async Task GetByFeatureIdAsync_Found_CallsRepo_ReturnsCategoryString()
        {
            // Arrange
            TagList mockTagList = new TagList
            {
                ForeignId = FeatureId,
                TagNames = new List<string> {
                    _mockCategory
                }
            };

            var mockTagsRepository = new Mock<ITagsRepository>();
            mockTagsRepository.Setup(r => r.GetByForeignIdAsync(It.IsAny<long>(),
                                                                TagAssetType.Feature,
                                                                TagContext.uexpress_subnavigations))
                .ReturnsAsync(mockTagList);

            var categoryService = new CategoriesService(mockTagsRepository.Object);

            // Act
            var category = await categoryService.GetByFeatureIdAsync(FeatureId);

            // Assert
            mockTagsRepository.Verify(r => r.GetByForeignIdAsync(FeatureId,
                                                                 TagAssetType.Feature,
                                                                 TagContext.uexpress_subnavigations), Times.Once());

            Assert.Equal(_mockCategory, category);
        }

        [Fact]
        public async Task GetByFeatureIdAsync_FoundMutipleCategories__ThrowsException()
        {
            // Arrange
            TagList mockTagList = new TagList
            {
                ForeignId = FeatureId,
                TagNames = new List<string> {
                    _mockCategory,
                    "shouldntHaveMultiple"
                }
            };

            var mockTagsRepository = new Mock<ITagsRepository>();
            mockTagsRepository.Setup(r => r.GetByForeignIdAsync(It.IsAny<long>(),
                                                                TagAssetType.Feature,
                                                                TagContext.uexpress_subnavigations))
                .ReturnsAsync(mockTagList);

            var categoryService = new CategoriesService(mockTagsRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => categoryService.GetByFeatureIdAsync(FeatureId));
        }

        [Fact]
        public async Task GetByFeatureIdAsync_FoundNoCategories__ThrowsException()
        {
            // Arrange
            TagList mockTagList = new TagList
            {
                ForeignId = FeatureId,
                TagNames = new List<string>()
            };

            var mockTagsRepository = new Mock<ITagsRepository>();
            mockTagsRepository.Setup(r => r.GetByForeignIdAsync(It.IsAny<long>(),
                                                                TagAssetType.Feature,
                                                                TagContext.uexpress_subnavigations))
                .ReturnsAsync(mockTagList);

            var categoryService = new CategoriesService(mockTagsRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<UexpressNotFoundException>(() => categoryService.GetByFeatureIdAsync(FeatureId));
        }
        */
    }
}
