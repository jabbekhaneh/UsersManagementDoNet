using UsersManagement;
using UsersManagement.TokenBase;
using UsersManagement.TokenBase.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.UserManagementTokenApiServices(options =>
{
    options.UseOption = UsersManagement.Options.UserManagementUseOption.Custome;
    options.ConnectionString = "data source =.; initial catalog =dbJabbekhaneh; integrated security = True; MultipleActiveResultSets=True";
});

builder.Services.UsersManagementTokenBaseService("jwtAuthSecureKey_Portal_API_$$$",
    option =>
{
    option.ConnectionString = "data source =.; initial catalog =dbJabbekhaneh; integrated security = True; MultipleActiveResultSets=True";
    option.DatabaseName = "dbJabbekhaneh";
    option.IsCreateAdminUser = true;
    option.Admin = new UserAdminOption("09107066676", "123456")
    {
        FirstName = "Hassan",
        LastName = "Jabbekhaneh",
        Email = "Jabbekhaneh@gmail.com",
        Mobile = "09107066676",
        Database = Database.MSQL,

    };
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
