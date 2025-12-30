using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
	config
	.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
	.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
});


builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = "Bearer";
})
	.AddJwtBearer("Bearer", options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters()
		{
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7XzyrJWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr")),
			ValidAudience = "all",
			ValidIssuer = "all",
			ValidateIssuerSigningKey = true,
			ValidateLifetime = true,
			ClockSkew = TimeSpan.Zero
		};
	});


builder.Services.AddOcelot();
builder.Services.AddControllers();

builder.Services.AddCors(options => {
	options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
});


var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());
app.UseAuthentication();
//app.UseAuthorization();

app.UseCors("CORSPolicy");
app.UseWebSockets();
app.UseOcelot().Wait();

app.MapGet("/", () => "Hello World!");
app.UseHttpsRedirection();
app.Run();
