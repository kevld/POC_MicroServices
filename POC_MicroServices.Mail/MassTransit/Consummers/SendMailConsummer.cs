using MassTransit;
using MassTransit.Interfaces;
using Microsoft.AspNetCore.SignalR;
using POC_MicroServices.Mail.Hub;
using POC_MicroServices.Mail.Repository;
using POC_MicroServices.Mail.Repository.Models;

namespace POC_MicroServices.Mail.MassTransit.Consummers
{
    public class SendMailConsummer : IConsumer<ISendMail>
    {
        private readonly MailDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public SendMailConsummer(MailDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<ISendMail> context)
        {
            try
            {
                ISendMail message = context.Message;
                MailEntity me = new()
                {
                    To = message.To,
                    Content = message.Content,
                    SendDate = DateTime.Now,
                };

                _context.Mails.Add(me);
                _context.SaveChanges();

                await _hubContext.Clients.All.SendAsync("notification", "Notification : Mail sent, woooooooooooohooooooooooo");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}