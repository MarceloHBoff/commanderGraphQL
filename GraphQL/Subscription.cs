using CommanderGraphQL.Model;
using HotChocolate;
using HotChocolate.Types;

namespace CommanderGraphQL.GraphQL
{
    public class Subscription
    {
        [Subscribe]
        [Topic]
        public Platform OnPlarformAdded([EventMessage] Platform platform) => platform;
    }
}