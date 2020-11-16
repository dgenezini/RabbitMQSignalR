using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RabbitMQSample.SignalRConsumer.Services;
using System.Threading.Tasks;

namespace RabbitMQSample.SignalRConsumer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly RabbitMqConsumerService _RabbitMqConsumerService;

        public IndexModel(ILogger<IndexModel> logger,
            RabbitMqConsumerService rabbitMqConsumerService)
        {
            _logger = logger;
            _RabbitMqConsumerService = rabbitMqConsumerService;
        }

        public async Task OnGet()
        {
            await _RabbitMqConsumerService.StartAsync();
        }
    }
}
