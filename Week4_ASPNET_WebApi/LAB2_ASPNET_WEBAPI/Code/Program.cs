using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SwaggerWebAPIDemo.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add Swagger with detailed configuration as per requirements
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Swagger Demo",
        Version = "v1",
        Description = "TBD",
        TermsOfService = new Uri("https://www.example.com/terms"),
        Contact = new OpenApiContact() 
        { 
            Name = "John Doe", 
            Email = "john@xyzmail.com", 
            Url = new Uri("https://www.example.com") 
        },
        License = new OpenApiLicense() 
        { 
            Name = "License Terms", 
            Url = new Uri("https://www.example.com/license") 
        }
    });
});

// Add In-Memory Database
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseInMemoryDatabase("ApiDatabase"));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // specifying the Swagger JSON endpoint.
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Demo");
        c.RoutePrefix = "swagger"; // Access at /swagger
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Seed database on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
    context.Database.EnsureCreated();
}

app.Run();
