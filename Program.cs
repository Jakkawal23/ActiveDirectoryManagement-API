using ActiveDirectoryManagement_API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("ActiveDirectoryManagementConnectionString"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors(options =>
//{
//    options.AllowAnyHeader();
//    options.AllowAnyOrigin();
//    options.AllowAnyMethod();
//});

app.UseCors(options =>
{
    options.AllowAnyHeader()
           .AllowAnyMethod()
           .SetIsOriginAllowed(_ => true); // Allow any origin
});


//app.UseCors(builder =>
//{
//    builder.AllowAnyHeader()
//           .AllowAnyOrigin()
//           .AllowAnyMethod();
//});

app.UseAuthorization();

app.MapControllers();

app.Run();
