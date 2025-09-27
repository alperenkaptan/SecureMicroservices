using IdentityServer;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddRazorPages();
        builder.Services.AddSwaggerGen();
        builder.Services.AddIdentityServer()
            .AddInMemoryClients(Config.GetClients())
            .AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddInMemoryApiResources(Config.GetApiResources())
            .AddInMemoryApiScopes(Config.GetApiScopes())
            .AddTestUsers(Config.GetTestUsers())
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