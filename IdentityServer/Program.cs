using IdentityServer;
using IdentityServerHost;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddRazorPages();
        builder.Services.AddSwaggerGen();
        builder.Services.AddIdentityServer()                                 //EFCORE a �evir
            .AddInMemoryClients(Config.GetClients())                         //EFCORE a �evir
            .AddInMemoryIdentityResources(Config.GetIdentityResources())     //EFCORE a �evir
            .AddInMemoryApiScopes(Config.GetApiScopes())                     //EFCORE a �evir
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