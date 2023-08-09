using CourseMicroservices.Services.Basket.Interfaces;
using CourseMicroservices.Services.Basket.Services;
using CourseMicroservices.Services.Basket.Settings;
using CourseMicroservices.Shared.Extantion;
using CourseMicroservices.Shared.Interfaces;
using CourseMicroservices.Shared.Services;
using CourseMicroservices.Shared.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService,SharedIdentityService>();
builder.Services.AddScoped<IBasketService, BasketService>();


builder.Services.Configure<RedisSetttings>(builder.Configuration.GetSection("RedisSettings"));

builder.Services.AddCustomTokenAuth(AudiencesName.BasketMicroservice,TokenVerifyScheme.BasketSchema);

builder.Services.AddScoped<RedisService>(sp =>
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisSetttings>>().Value;
    var redis = new RedisService(redisSettings.Host, redisSettings.Port);
    redis.Connect();
    return redis;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
