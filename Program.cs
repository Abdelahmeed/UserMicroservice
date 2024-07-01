using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UserMicroservice.Data;
using UserMicroservice.Repositories;
using UserMicroservice.Services;
using Microsoft.Graph;
using Azure.Identity;
using Azure.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "UserMicroservice_";
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"{builder.Configuration["AzureAd:Instance"]}{builder.Configuration["AzureAd:TenantId"]}";
        options.Audience = builder.Configuration["AzureAd:ClientId"];
    });

builder.Services.AddAuthorization();

builder.Services.AddSingleton<TokenCredential>(sp =>
{
    var config = builder.Configuration;
    return new ClientSecretCredential(
        config["AzureAd:TenantId"],
        config["AzureAd:ClientId"],
        config["AzureAd:ClientSecret"]
    );
});

builder.Services.AddScoped<GraphServiceClient>(sp =>
{
    var tokenCredential = sp.GetRequiredService<TokenCredential>();
    return new GraphServiceClient(tokenCredential);
});

builder.Services.AddScoped<IGraphService, GraphService>();

// Configure Swagger (optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
