using System.Text;
using API;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using API.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
 builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AdIdentityServices(builder.Configuration);

var app = builder.Build();
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().
    WithOrigins("https://localhost:4200"));
 // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
