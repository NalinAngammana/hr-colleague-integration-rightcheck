using ColleagueInt.RTW.Database;
using ColleagueInt.RTW.Test.Repositories.SeedTestData;
using Microsoft.EntityFrameworkCore;

namespace ColleagueInt.RTW.Test.Repositories
{
    internal static class ContextCreator
    {
        internal static RTWContext CreateContextWithSeedData(string contextName)
        {
            var options = new DbContextOptionsBuilder<RTWContext>()
                .UseInMemoryDatabase(databaseName: contextName)
                .Options;
            using (var context = new RTWContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var seedData = new SeedData(context);
                seedData.ClearDatabase();
                seedData.LoadSeedData();
            }

            var rtwcontext = new RTWContext(options);
            return rtwcontext;
        }
    }
}
