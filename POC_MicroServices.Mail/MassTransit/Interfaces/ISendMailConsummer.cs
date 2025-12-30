namespace POC_MicroServices.Mail.MassTransit.Interfaces
{
    public interface ISendMailConsummer
    {
        public string To { get; set; }

        public string Content { get; set; }
    }
}
