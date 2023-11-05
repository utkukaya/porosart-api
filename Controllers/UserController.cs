using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using porosartapi.model.ViewModels;
using porosartapi.Services;
using porosartapi.model.ViewModel;

namespace porosartapi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost(Name = "Login")]

    public ResponseVM<UserVM> Login([FromBody] LoginVM userParam)
    {
        var response = _userService.Login(userParam.Username, userParam.Password);

        if (!response.IsSuccess)
            return new ResponseVM<UserVM>("Username or password is incorrect");

        return response;
    }

    [AllowAnonymous]
    [HttpPost(Name = "Register")]
    public ResponseVM Register([FromBody] UserFormVM newUser)
    {
        if (!newUser.isValidPassword() || newUser.isEmptyPassword())
            return new ResponseVM("Password should contain minimum eight characters, at least one uppercase letter, one lowercase letter and one number!");
        return _userService.SaveUser(newUser);
    }
}
