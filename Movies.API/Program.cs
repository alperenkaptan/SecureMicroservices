using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Movies.API.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MoviesAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesAPIContext") ?? throw new InvalidOperationException("Connection string 'MoviesAPIContext' not found.")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Jwt:Authority"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
            //ValidateIssuer = true,
            //ValidateAudience = true,
            //ValidateLifetime = true,
            //ValidateIssuerSigningKey = true,
            //ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //ValidAudience = builder.Configuration["Jwt:Audience"],
            //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ClientIdPolicy", policy => policy
    .RequireClaim("client_id", "movieClient"));
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MoviesAPIContext>();
    await MoviesContextSeed.SeedAsync(context);
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
