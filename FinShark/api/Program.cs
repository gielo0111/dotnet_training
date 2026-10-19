using api.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Generates the raw /openapi/v1.json data file that both UIs read from
    app.MapOpenApi();
    
    // 2. Map the modern Scalar dashboard
    app.MapScalarApiReference();
    
    // 3. Map the classic Swagger dashboard
    app.UseSwaggerUI(options => 
    {
        options.SwaggerEndpoint("/openapi/v1.json", "FinShark API v1");
    });
}

app.UseHttpsRedirection();

app.Run();