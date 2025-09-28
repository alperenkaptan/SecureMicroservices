using Duende.IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Movies.Client.HttpHandler;
using Movies.Client.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("IdentityServerClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5050");
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});

builder.Services.AddTransient<AuthenticationDelegatingHandler>();
builder.Services.AddHttpClient("MoviesAPIClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5052"); //ocelot gateway
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
}).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
  {
      options.Authority = "https://localhost:5050";
      options.ClientId = "movieClientInteractive";
      options.ClientSecret = "secret";
      options.ResponseType = "code id_token"; //hybrid flow
      options.SaveTokens = true;
      options.GetClaimsFromUserInfoEndpoint = true;
      options.Scope.Add("movieAPI");
      options.Scope.Add("address");
      options.Scope.Add("email");

      options.Scope.Add("roles");
      options.ClaimActions.MapUniqueJsonKey("role", "role");

      options.TokenValidationParameters = new TokenValidationParameters
      {
          NameClaimType = JwtClaimTypes.Name,
          RoleClaimType = JwtClaimTypes.Role
      };
  });

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.Run();
