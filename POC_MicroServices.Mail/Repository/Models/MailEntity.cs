using System.ComponentModel.DataAnnotations;

namespace POC_MicroServices.Mail.Repository.Models
{
    public class MailEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string To { get; set; }

        public string Content { get; set; }

        public DateTime SendDate { get; set; }
    }
}
