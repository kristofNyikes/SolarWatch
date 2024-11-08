using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SolarWatch.Contracts;
using SolarWatch.Services.Authentication;

namespace SolarWatch.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authenticationService;
    //private readonly int _cookiesExpirationMinutes;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(IAuthService authenticationService, IConfiguration configuration, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _authenticationService = authenticationService;
        _signInManager = signInManager;
        _userManager = userManager;
        //_cookiesExpirationMinutes = configuration.GetValue<int>("AuthSettings:CookiesExpirationMinutes");

    }

    [HttpPost("Register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authenticationService.RegisterAsync(request.Email, request.Username, request.Password, "User");

        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }

        return CreatedAtAction(nameof(Register), new RegistrationResponse(result.Email, result.UserName));
    }

    [HttpPost("Login")]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            ModelState.AddModelError("Login", "Invalid email or password");
            return BadRequest(ModelState);
        }

        var result =
            await _signInManager.PasswordSignInAsync(user, request.Password, isPersistent: true,
                lockoutOnFailure: false);

        var role = await _userManager.GetRolesAsync(user);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("Login", "Invalid email or password");
        }

        var response = new AuthResponse(true, user.Email, user.UserName);
        return Ok(new { Response = response, Role = role });
    }

    [HttpPost("Logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete(".AspNetCore.Identity.Application");
        return Ok();
    }

    private void AddErrors(AuthResult result)
    {
        foreach (var error in result.ErrorMessages)
        {
            ModelState.AddModelError(error.Key, error.Value);
        }
    }
}