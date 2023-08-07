using CourseMicroservices.Services.Order.Application.Extantion;
using CourseMicroservices.Services.Order.Application.Queries.Order;
using CourseMicroservices.Services.Order.Instrastructure;
using CourseMicroservices.Shared.Extantion;
using CourseMicroservices.Shared.Interfaces;
using CourseMicroservices.Shared.Services;
using CourseMicroservices.Shared.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddCustomTokenAuth(AudiencesName.OrderMicroservice);
builder.Services.AddMediatR(typeof(GetOrderByUserIdQuery));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

builder.Services.AddDbContext<OrderDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), configure =>
    {
        configure.MigrationsAssembly("CourseMicroservices.Services.Order.Instrastructure");
    });
});


builder.Services.AddAutoMapperExtantiton();
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
