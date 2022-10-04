using Microsoft.AspNetCore.Mvc;
using UsersManagement.TokenBase.Common;
using UsersManagement.TokenBase.Models;

namespace UsersManagement.Sample.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserManagementController : ControllerBase
{

    private readonly IUserMangementService _repository;

    public UserManagementController(IUserMangementService repository)
    {
        _repository = repository;
    }

   
    [HttpPost]
    public async Task<IActionResult> Insert()
    {
        
        return Ok();
    }
}
