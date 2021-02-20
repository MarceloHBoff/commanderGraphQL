using System.Threading;
using System.Threading.Tasks;
using CommanderGraphQL.Database;
using CommanderGraphQL.GraphQL.Types;
using CommanderGraphQL.Model;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;

namespace CommanderGraphQL.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(
            AddPlatformInput input,
            [ScopedService] AppDbContext context,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            var platform = new Platform
            {
                Name = input.Name
            };

            context.Platform.Add(platform);
            await context.SaveChangesAsync();

            await eventSender.SendAsync(nameof(Subscription.OnPlarformAdded), platform, cancellationToken);

            return new AddPlatformPayload(platform);
        }

        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService] AppDbContext context)
        {
            var command = new Command
            {
                HowTo = input.HowTo,
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId
            };

            context.Command.Add(command);
            await context.SaveChangesAsync();

            return new AddCommandPayload(command);
        }
    }
}