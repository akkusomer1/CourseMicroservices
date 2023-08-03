using CourseMicroservices.Services.Discount.Interfaces;
using CourseMicroservices.Services.Discount.Services;
using CourseMicroservices.Shared.Extantion;
using CourseMicroservices.Shared.Interfaces;
using CourseMicroservices.Shared.Services;
using CourseMicroservices.Shared.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCustomTokenAuth(AudiencesName.DiscountMicroservice);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ISharedIdentityService,SharedIdentityService>();
builder.Services.AddScoped<IDiscountService,DiscountService>();
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
