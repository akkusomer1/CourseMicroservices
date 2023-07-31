﻿using CourseMicroservices.Shared.Services;
using CourseMicroservices.Shared.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseMicroservices.Shared.Extantion
{
    public static class CustomTokenAuth
    {
        public static void AddCustomTokenAuth(this IServiceCollection Services, string audience)
        {
            Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {

                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = CustomTokenOptions.Issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = SignService.GetSymmetricSecurityKey(CustomTokenOptions.SecurityKey),

                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
