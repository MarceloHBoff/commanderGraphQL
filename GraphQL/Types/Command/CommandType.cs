using System.Linq;
using CommanderGraphQL.Database;
using CommanderGraphQL.Model;
using HotChocolate;
using HotChocolate.Types;

namespace CommanderGraphQL.GraphQL.Types
{
    public class CommandType : ObjectType<Command>
    {
        protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
        {
            descriptor.Description("Represents any executable command");

            descriptor.Field(p => p.Platform)
                .ResolveWith<Resolvers>(p => p.GetPlatform(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("This is the platform to which the command belongs");
        }

        private class Resolvers
        {
            public Platform GetPlatform(Command command, [ScopedService] AppDbContext context)
            {
                return context.Platform.FirstOrDefault(p => p.Id == command.PlatformId);
            }
        }
    }
}