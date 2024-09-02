using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WritersPlatform.DataAccess.Entities;
using WritersPlatform.Models;

namespace WritersPlatform.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class AccountController : Controller
{
    private readonly UserManager<AppUser> userManager;
    private readonly SignInManager<AppUser> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
    }

    [HttpGet("Login")]
    [AllowAnonymous]
    public IActionResult Login(string returnUrl = "/")
    {
        var model = new LoginModel { ReturnUrl = returnUrl };
        return View(model);
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromForm] LoginModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                await signInManager.SignOutAsync();
                var result = await signInManager.PasswordSignInAsync(
                    user,
                    model.Password,
                    model.Remember,
                    false);
                if (result.Succeeded)
                {
                    return Redirect(model.ReturnUrl ?? "/");
                }
            }
            ModelState.AddModelError(nameof(model.Email), "Неверный пользователь");
        }
        return View();
    }

    [HttpGet("Register")]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromForm]RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            var checkEmailUser = await userManager.FindByEmailAsync(model.Email);
            if (checkEmailUser != null)
            {
                ModelState.AddModelError(string.Empty, "Такая почта уже используется");
            }
            else
            {
                await signInManager.SignOutAsync();
                AppUser user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);

                    await userManager.AddToRolesAsync(user, new List<string>() { "user"});
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
        }
        return View(model);
    }

    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("AccessDenied")]
    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpGet("GoogleLogin")]
    [AllowAnonymous]
    public IActionResult GoogleLogin()
    {
        var redirectUrl = Url.Action(nameof(GoogleResponse), "Account");
        var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
        return new ChallengeResult("Google", properties);
    }

    [HttpGet("GoogleResponse")]
    [AllowAnonymous]
    public async Task<IActionResult> GoogleResponse()
    {
        var info = await signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            return RedirectToAction(nameof(Login));
        }

        var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
        string[] userInfo = {info.Principal.FindFirst(ClaimTypes.Name).Value,
            info.Principal.FindFirst(ClaimTypes.Email).Value };

        if (result.Succeeded)
        {
            return View(userInfo);
        }
        var user = new AppUser()
        {
            Email = userInfo.Last(),
            UserName = userInfo.Last(),
        };

        var identityResult = await userManager.CreateAsync(user);
        if (identityResult.Succeeded)
        {
            identityResult = await userManager.AddLoginAsync(user, info);
            if (identityResult.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                await userManager.AddToRolesAsync(user, new List<string>() { "user" });
                return View(userInfo);
            }
        }
        return AccessDenied();
    }

    [HttpGet("Delete")]
    public async Task<IActionResult> Delete()
    {
        var user = await userManager.GetUserAsync(User);
        if (user != null)
        {
            IdentityResult result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Logout", "Account");
            }
        }
        return RedirectToAction("Index", "Home");
    }
}
