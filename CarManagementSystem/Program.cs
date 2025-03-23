using CarManagementSystem.Middleware;
using CarManagementSystem.Repositories;
using CarManagementSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecific", builder =>
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = "CarModelMgmt-issuer",
                ValidAudience = "CarModelMgmt-audience",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("d!X4w9zB3rQf#NpLmV@7^Yt$C2gH8K5M"))
            };
        });

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarManagementSystem", Version = "v1" });
});

builder.Services.AddScoped<ICarModelRepository, CarModelRepository>();
builder.Services.AddScoped<ISalesmanCommissionRepository, SalesmanCommissionRepository>();
builder.Services.AddScoped<ISalesDataRepository, SalesDataRepository>();
builder.Services.AddScoped<ICarModelService, CarModelService>();
builder.Services.AddScoped<ISalesmanCommissionService, SalesmanCommissionService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISalesDataService, SalesDataService>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarManagementSystem v1"));
}

app.UseRouting();
app.UseCors("AllowSpecific");
app.UseAuthentication();
app.UseAuthorization();

app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true
});

app.MapControllers();

app.Run();
