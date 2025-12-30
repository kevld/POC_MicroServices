using MassTransit;
using POC_MicroServices.Mail.MassTransit.Messages;
using POC_MicroServices.Mail.Repository;
using POC_MicroServices.Mail.Repository.Models;

namespace POC_MicroServices.Mail.MassTransit.Consummers
{
    public class SendMailConsummer : IConsumer<IMailMessage>
    {
        private readonly MailDbContext _context;

        public SendMailConsummer(MailDbContext context)
        {
            _context = context;
        }

        public Task Consume(ConsumeContext<IMailMessage> context)
        {
            IMailMessage message = context.Message;
            MailEntity me = new()
            {
                To = message.To,
                Content = message.Content,
                SendDate = message.SendDate,
            };

            _context.Mails.Add(me);
            _context.SaveChanges();

            return Task.CompletedTask;
        }
    }
}