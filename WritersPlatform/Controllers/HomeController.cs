using WritersPlatform.DataAccess.Entities;
using WritersPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WritersPlatform.Models;

namespace WritersPlatform.Controllers;

[Route("[controller]")]
[ApiController]
public class HomeController : Controller
{
    private readonly UserManager<AppUser> userManager;
    private readonly ILogger<HomeController> logger;

    public HomeController(UserManager<AppUser> userManager,ILogger<HomeController> logger)
    {
        this.userManager = userManager;
        this.logger = logger;
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("Privacy")]
    [Authorize]
    public async Task<IActionResult> Privacy()
    {
        var user = await userManager.GetUserAsync(HttpContext.User);
        var message = "Hello " + user?.UserName;
        return Content(message);
    }

    [Authorize]
    [HttpGet("UserData")]
    public async Task<IActionResult> UserData()
    {
        var user = await userManager.GetUserAsync(User);
        var model = new ProfileModel()
        {
            UserName = user.UserName,
            Email = user.Email,
        };

        return View(model);
    }

    [HttpGet("Error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
