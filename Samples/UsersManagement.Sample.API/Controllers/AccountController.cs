using Microsoft.AspNetCore.Mvc;
using UsersManagement.Common;
using UsersManagement.Models.DTOs;
using UsersManagement.Sample.API.Models;

namespace UsersManagement.Sample.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        #region Cons
        private readonly IUserManagement _services;

        public AccountController(IUserManagement services)
        {
            _services = services;
        }
        #endregion

        #region Actions
        //---------------------------
        [HttpPost("[ACTION]")]
        public async Task<IActionResult> RegisterMobileUsername(RegisterMobileUsername register)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var CconfirmCode = _services.CodeGenerator(4);
            var signUp = await _services.SignUpAsync(register.Mobile, new SignUpDto
            {
                ConfimCode = CconfirmCode,
                Mobile = register.Mobile,

            });
            string msg = string.Empty;
            bool isSuccess;
            isSuccess = CheckValidationStatus(signUp, ref msg);
            if (!isSuccess)
                return BadRequest(new { success = isSuccess, msg = msg });
            //ToDo Send SMS
            return Ok(new { success = isSuccess, msg = msg });
        }
        //---------------------------
        [HttpPost("[ACTION]")]
        public async Task<IActionResult> RegisterEmailUsername(RegisterEmailUsername register)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var signUp = await _services.SignUpAsync(register.Email, new SignUpDto
            {
                Password=register.Password,
                Email = register.Email,
            });
            string msg = string.Empty;
            bool isSuccess;
            isSuccess = CheckValidationStatus(signUp, ref msg);
            if (!isSuccess)
                return BadRequest(new { success = isSuccess, msg = msg });

            var token = await _services.TokenGenerator(signUp.userId, DateTime.Now.AddDays(1));
            //ToDoSend Email
            return Ok(new { success = true, msg = token });
        }
        //---------------------------
        #endregion


        #region Private Methods
        //---------------------------
        private static bool CheckValidationStatus((SignUpStatus signUpStatus, Guid userId) signUp, ref string msg)
        {
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

            return isSuccess;
        }
        //---------------------------
        #endregion


    }
}
