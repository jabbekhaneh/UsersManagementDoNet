using Microsoft.OpenApi.Models;
using UsersManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.UserManagementTokenApiServices(options =>
{
    options.UseOption = UsersManagement.Options.UserManagementUseOption.Custome;
    options.ConnectionString = "";
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseApplicationUserManagement();
app.MapControllers();

app.Run();
