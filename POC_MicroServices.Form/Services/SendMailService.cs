using MassTransit;
using MassTransit.Interfaces;

namespace POC_MicroServices.Form.Services
{
    public class SendMailService
    {
        private readonly IBus _bus;
        private const string rabbitUri = "rabbitmq://localhost/sendMailQueue";

        public SendMailService(IBus bus)
        {
            _bus = bus;
        }

        public async Task SendMailAsync(string to, string content)
        {
            Uri busUri = new Uri(rabbitUri);

            var endPoint = await _bus.GetSendEndpoint(busUri);
            await endPoint.Send<ISendMail>(new
            {
                To = to,
                Content = content
            });
        }
    }
}
