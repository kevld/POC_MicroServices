namespace POC_MicroServices.Mail.MassTransit.Messages
{
    public interface IMailMessage
    {
        public string To { get; set; }

        public string Content { get; set; }

        public DateTime SendDate { get; set; }
    }
}
