using Microsoft.AspNetCore.Mvc;
using UsersManagement.Sample.API.Models;
using UsersManagement.TokenBase.Common;
using UsersManagement.TokenBase.DTOs;

namespace UsersManagement.Sample.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserManagementController : ControllerBase
{

    private readonly IUserMangementService _services;

    public UserManagementController(IUserMangementService services)
    {
        _services = services;
    }

    //---------------------------
    [HttpPost("[ACTION]")]
    public async Task<IActionResult> RegisterMobileUsername(RegisterMobileUsername register)
    {
        var CconfirmCode = _services.GenerateCode(4);
        var signUp= await _services.SignUpAsync(register.Mobile, new SignUpDto
        {
            ConfimCode=CconfirmCode,
            FirstName = register.FirstName,
            LastName = register.LastName,
            Mobile = register.Mobile,
            Email = register.Email,
        });
        string msg = string.Empty;
        bool isSuccess;
        switch (signUp.signUpStatus)
        {
            case SignUpStatus.DublicateUsername:
                msg = "dublicate user name";
                isSuccess = false;
                break;
            case SignUpStatus.CreateUserSuccess:
                msg = "successFully is register";
                isSuccess = true;
                break;
            default:
                isSuccess = false;
                break;
        }
        if(!isSuccess)
            return BadRequest(new {success = isSuccess ,msg =msg });
        //ToDo Send SMS
        return Ok(new { success = isSuccess, msg = msg });
    }
    //---------------------------


}
