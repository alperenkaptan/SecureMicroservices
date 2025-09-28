using IdentityServer;
using IdentityServerHost;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddRazorPages();
        builder.Services.AddSwaggerGen();
        builder.Services.AddIdentityServer()                                 //EFCORE a çevir
            .AddInMemoryClients(Config.GetClients())                         //EFCORE a çevir
            .AddInMemoryIdentityResources(Config.GetIdentityResources())     //EFCORE a çevir
            .AddInMemoryApiScopes(Config.GetApiScopes())                     //EFCORE a çevir
            .AddTestUsers(TestUsers.Users)
            .AddDeveloperSigningCredential();

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
        }
        app.UseStaticFiles();
        app.UseIdentityServer();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.MapRazorPages();
        app.Run();
    }
}