using UsersManagement;
using UsersManagement.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddUserManagement(
    option =>
{
    option.Database = DatabaseType.MSQL;
    //option.Key = "jwtAuthSecureKey_Portal_API_$$$";
    option.ConnectionString = "data source =.; initial catalog =dbJabbekhaneh; integrated security = True; MultipleActiveResultSets=True";
    option.DatabaseName = "dbJabbekhaneh";
    option.IsCreateAdminUser = true;
    option.Admin = new UserAdminOption("09107066676", "123456")
    {
        FirstName = "Hassan",
        LastName = "Jabbekhaneh",
        Email = "Jabbekhaneh@gmail.com",
        Mobile = "09107066676",

    };
});
builder.Services.AddUserManagementTokenBase();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseApplicationUserManagement(app.Environment.IsDevelopment());
app.MapControllers();

app.Run();
