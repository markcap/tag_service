namespace TagService.UnitTests
{
    using Microsoft.EntityFrameworkCore;
    using TagService.Models;

    public static class DbContextMocker
    {
        public static TagDbContext GetTagDbContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<TagDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new TagDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            // Add entities in memory
            dbContext.Seed();

            return dbContext;
        }
    }
}
