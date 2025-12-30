using Microsoft.EntityFrameworkCore;
using POC_MicroServices.Mail.Repository.Models;

namespace POC_MicroServices.Mail.Repository
{
    public class MailDbContext : DbContext
    {
        public DbSet<MailEntity> Mails { get; set; }

        public MailDbContext(DbContextOptions<MailDbContext> options) : base(options)
        {
		}
	}
}
