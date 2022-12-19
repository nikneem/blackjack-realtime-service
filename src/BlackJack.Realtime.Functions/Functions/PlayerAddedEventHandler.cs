using Azure.Messaging.EventGrid;
using BlackJack.Events.Players;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BlackJack.Realtime.Functions.Functions
{
    public class PlayerAddedEventHandler
    {
        private readonly ILogger _logger;

        public PlayerAddedEventHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PlayerAddedEventHandler>();
        }

        [Function("PlayerAddedEventHandler")]
        public void Run(
            [EventGridTrigger] EventGridEvent input
            )
        {
            var eventInfo = input.Data.ToObjectFromJson<BlackJackPlayerAddedEvent>();
            
        }
    }

}
