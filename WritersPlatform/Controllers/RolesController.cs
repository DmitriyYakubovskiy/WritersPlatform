using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WritersPlatform.DataAccess.Entities;
using WritersPlatform.ViewModels;

namespace WritersPlatform.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly UserManager<AppUser> userManager;

    public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
    {;
        this.roleManager = roleManager;
        this.userManager = userManager;
    }

    [HttpGet("List")]
    [Authorize(Roles = "admin")]
    public IActionResult List()
    {
        return View(roleManager.Roles.ToArray());
    }

    [HttpGet("Create")]
    [Authorize(Roles = "admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromForm] string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
            if (result.Succeeded)
            {
                return RedirectToAction("/");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        return RedirectToAction("/");
    }

    [HttpPost("Delete")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(string id)
    {
        IdentityRole role = await roleManager.FindByIdAsync(id);
        if (role != null)
        {
            IdentityResult result = await roleManager.DeleteAsync(role);
        }
        return RedirectToAction("Index");
    }

    [HttpGet("UserList")]
    [Authorize(Roles = "admin")]
    public IActionResult UserList()
    {
        return View(userManager.Users.ToArray());
    }

    [HttpGet("Edit")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var userRoles = await userManager.GetRolesAsync(user);
            var allRoles = roleManager.Roles.ToList();
            ChangeRoleViewModel model = new ChangeRoleViewModel
            {
                UserId = user.Id,
                UserEmail = user.Email!,
                UserRoles = userRoles,
                AllRoles = allRoles
            };
            return View(model);
        }

        return NotFound();
    }

    [HttpPost("Edit")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit([FromForm] string userId, [FromForm] List<string> roles)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var userRoles = await userManager.GetRolesAsync(user);
            var allRoles = roleManager.Roles.ToList();
            var addedRoles = roles.Except(userRoles);
            var removedRoles = userRoles.Except(roles);

            await userManager.AddToRolesAsync(user, addedRoles);
            await userManager.RemoveFromRolesAsync(user, removedRoles);

            return RedirectToAction("List");
        }

        return NotFound();
    }
}
