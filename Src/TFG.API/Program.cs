using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TFG.Application;
using TFG.Application.Contracts.Service;
using TFG.API.Migrations;
using TFG.Infrastructure;

var builder = WebApplication.CreateBuilder (args);

// Add services to the container.

builder.Services.AddControllers ();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen ();
builder.Services.AddAutoMapper (typeof (Program));
builder.Services.AddApplicationServices ()
    .AddApplicationInfrastructureServices (builder.Configuration)
    .AddScoped<RoleMigration> ();

var issuer = builder.Configuration.GetValue<string> ("baseUrl");
var key = builder.Configuration.GetValue<string> ("jwtSecret");

builder.Services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer (options => {
        options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = issuer,
        IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (key))
        };

        options.Events = new JwtBearerEvents {
            OnAuthenticationFailed = context => {
                if (context.Exception.GetType () == typeof (SecurityTokenExpiredException)) {
                    context.Response.Headers.Add ("Token-Expired", "true");
                }
                return Task.CompletedTask;
            }
        };
    });

var roleMigration = builder.Services.BuildServiceProvider ().GetRequiredService<RoleMigration> ();

roleMigration.CreateRoles ().Wait ();

var app = builder.Build ();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment ()) {
    app.UseSwagger ();
    app.UseSwaggerUI ();
}

app.UseExceptionHandler (builder => {
    builder.Run (async context => {
        context.Response.ContentType = "application/json";
        var exception = context.Features.Get<IExceptionHandlerFeature> ();

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync (JsonConvert
            .SerializeObject (new { errors = exception.Error.Message }));

    });
});

app.UseHttpsRedirection ();

app.UseAuthentication ();
app.UseAuthorization ();

app.MapControllers ();

app.Run ();