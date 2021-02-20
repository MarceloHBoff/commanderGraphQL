using System.Linq;
using CommanderGraphQL.Database;
using CommanderGraphQL.Model;
using HotChocolate;
using HotChocolate.Data;

namespace CommanderGraphQL.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext context)
        {
            return context.Platform;
        }

        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Command> GetCommands([ScopedService] AppDbContext context)
        {
            return context.Command;
        }
    }
}