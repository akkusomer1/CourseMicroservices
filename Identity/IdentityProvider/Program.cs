using CourseMicroservices.Shared.Services;
using IdentityProvider.Interfaces;
using IdentityProvider.Models;
using IdentityProvider.Services;
using IdentityProvider.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<IdentityAppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("IdentityLocalDb"), opt =>
    {
        opt.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
    });
});

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 6;
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;

}).AddEntityFrameworkStores<IdentityAppDbContext>();



builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    var tokenOptions = builder.Configuration.GetSection("CustomTokenOptions").Get<CustomTokenOptions>();

    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = tokenOptions!.Issuer,
        ValidAudience = tokenOptions!.Audiences[0],      
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions!.SecurityKey),
        
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,

        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITokenService, TokenService>();


builder.Services.Configure<CustomTokenOptions>(builder.Configuration.GetSection("CustomTokenOptions"));
builder.Services.AddSingleton<ICustomTokenOptions>(sp =>
{
    return sp.GetRequiredService<IOptions<CustomTokenOptions>>().Value;
});
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<IdentityAppDbContext>();
//    db.Database.Migrate();

//    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

//    if (!userManager.Users.Any())
//    {
//        userManager.CreateAsync(new AppUser
//        {
//            UserName = "akkus11",
//            Email = "akkus11@gmail.com",
//            City = "Malatya",
//        }, "Password12*").Wait();
//    }
//}


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
