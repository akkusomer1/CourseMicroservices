using CourseMicroservices.Services.Catalog.Interfaces;
using CourseMicroservices.Services.Catalog.Services;
using CourseMicroservices.Services.Catalog.Settings;
using CourseMicroservices.Shared.Extantion;
using CourseMicroservices.Shared.Settings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.Configure<DatabaseSettnings>(builder.Configuration.GetSection(nameof(DatabaseSettnings)));

builder.Services.AddCustomTokenAuth(AudiencesName.CatalogMicroservice,TokenVerifyScheme.CatalogSchema);

builder.Services.AddSingleton<IDatabaseSettnings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettnings>>().Value;
});
builder.Services.AddAutoMapper(typeof(Program));
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
