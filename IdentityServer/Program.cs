using IdentityServer;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();

        // Configure SQL Server connection string
        var connectionString = builder.Configuration.GetConnectionString("IdentityServerDb")
            ?? throw new InvalidOperationException("Connection string 'IdentityServerDb' not found.");

        // Add IdentityServer with EF stores
        builder.Services.AddIdentityServer()
            .AddInMemoryClients(Config.GetClients())
            .AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddInMemoryApiResources(Config.GetApiResources())
            .AddInMemoryApiScopes(Config.GetApiScopes())
            .AddTestUsers(Config.GetTestUsers())
            .AddDeveloperSigningCredential(); // For dev only

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
        }

        app.UseIdentityServer();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}