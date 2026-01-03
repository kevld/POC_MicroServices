namespace MassTransit.Interfaces
{
    public interface ISendMail
    {
        public string To { get; set; }

        public string Content { get; set; }
    }
}
