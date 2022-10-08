using Microsoft.AspNetCore.Mvc;
using UsersManagement.Common;

namespace UsersManagement.Sample.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserManagementController : ControllerBase
{

    private readonly IUserManagement _services;

    public UserManagementController(IUserManagement services)
    {
        _services = services;
    }

   
   


}
