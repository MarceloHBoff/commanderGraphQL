using System.Linq;
using CommanderGraphQL.Database;
using CommanderGraphQL.Model;
using HotChocolate;
using HotChocolate.Types;

namespace CommanderGraphQL.GraphQL.Types
{
    public class PlatformType : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor.Description("Represents any software or service that has a CLI");

            descriptor.Field(p => p.LicenseKey).Ignore();

            descriptor.Field(p => p.Commands)
                .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("This is the list of available commands for this platform");
        }

        private class Resolvers
        {
            public IQueryable<Command> GetCommands(Platform platform, [ScopedService] AppDbContext context)
            {
                return context.Command.Where(p => p.PlatformId == platform.Id);
            }
        }
    }
}