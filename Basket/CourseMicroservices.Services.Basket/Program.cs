using CourseMicroservices.Services.Basket.Services;
using CourseMicroservices.Services.Basket.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.Configure<RedisSetttings>(builder.Configuration.GetSection("RedisSetttings"));

//Dikkat ben ServiceProvider üzerinden DI container'a eklenmiþ service'leri alabiliyorum sonuç olarak IOptions<RedisSetttings> 'da D:I eklenmiþtir bunuda alabilirim.
//Ve connect metodunu çalýþtýrýyorum.
builder.Services.AddSingleton<RedisService>(sp =>
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

app.UseAuthorization();

app.MapControllers();

app.Run();
