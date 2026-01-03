using MassTransit;
using Microsoft.EntityFrameworkCore;
using POC_MicroServices.Mail.Hub;
using POC_MicroServices.Mail.MassTransit.Consummers;
using POC_MicroServices.Mail.Repository;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<MailDbContext>(options => options.UseSqlite($"Data Source={configuration.GetConnectionString("db")}"));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SendMailConsummer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("sendMailQueue", e =>
        {
            e.PrefetchCount = 16;
            e.UseMessageRetry(r => r.Interval(2, 100));
            e.ConfigureConsumer<SendMailConsummer>(context);
        });
    });
});

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationHub>("/hub");

app.Run();
