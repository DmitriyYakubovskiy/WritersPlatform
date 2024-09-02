using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.IO;
using WritersPlatform.DataAccess.Entities;
using WritersPlatform.Models;
using WritersPlatform.Services;
using WritersPlatform.ViewModels;

namespace WritersPlatform.Controllers;

[Route("[controller]")]
[ApiController]
public class CompositionController : Controller
{
    private readonly UserManager<AppUser> userManager;
    private readonly ICompositionService compositionService;
    private readonly IGenreService genreService;
    private readonly IAuthorService authorService;
    private readonly ICommentService commentService;

    public CompositionController(UserManager<AppUser> userManager, ICompositionService compositionService, IGenreService genreService, IAuthorService authorService, ICommentService commentService)
    {
        this.userManager = userManager;
        this.compositionService = compositionService;
        this.genreService = genreService;
        this.authorService = authorService;
        this.commentService = commentService;
    }

    [HttpGet("List")]
    public async Task<IActionResult> List(int? genreId=null, int? authorId = null, bool? earlier = null, bool? ratingHigher = null)
    {
        var compositions = new CompositionModel[0];

        if(genreId.HasValue || authorId.HasValue||earlier.HasValue||ratingHigher.HasValue) compositions=compositionService.SearchCompositions(genreId, authorId, earlier, ratingHigher);
        else compositions = compositionService.GetAllOnlyRead();

        var sortModel = new SortCompositionModel()
        {
            GenreId=genreId,
            AuthorId=authorId,
            Earlier=earlier,
            RatingHigher=ratingHigher
        };

        var user = await userManager.GetUserAsync(User);
        string email = user == null ? "" : user.Email!;
        return View((compositions, genreService.GetAll(), authorService.GetAll(), sortModel, email));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var user = await userManager.GetUserAsync(User);
        var model = compositionService.GetByIdOnlyRead(id);
        var comments = commentService.GetCommentsFromComposition(model.Id);
        int userId = -1;

        if (model == null) return NotFound(id);

        if (user != null)
        {
            if (authorService.GetByEmail(user!.Email!) == null)
                authorService.Create(new AuthorModel { UserId = user!.Id!, Email = user.Email! });

            userId = authorService.GetByEmail(user!.Email!).Id;
        }

        var compositionViewModel = new CompositionViewModel()
        {
            Composition = model,
            Comments = comments,
            AuthorId = userId
        };
        return View("Details", compositionViewModel);
    }

    [HttpGet("Create")]
    [Authorize]
    public async Task<IActionResult> Create()
    {
        var user= await userManager.GetUserAsync(User);
        if (authorService.GetByEmail(user!.Email!)==null)
            authorService.Create(new AuthorModel { UserId = user!.Id!, Email = user.Email! });
        var compositionViewModel = new CreateCompositionViewModel()
        {
            Composition = new CompositionModel
            {
                Author = authorService.GetByEmail(user!.Email!)
            },
            Genres = genreService.GetAll().ToArray(),
        };

        return View("Create", compositionViewModel);
    }

    [HttpPost("Create")]
    [Authorize]
    public async Task<IActionResult> Create([FromForm(Name ="name")] string name, IFormFile image, IFormFile file, [FromForm(Name = "description")] string description, [FromForm(Name ="genreId")]int genreId, [FromForm(Name = "authorId")] int authorId)
    {
        var fileName= String.Format(@"{0}.pdf", Guid.NewGuid());
        var imageName= String.Format(@"{0}."+$"{System.IO.Path.GetExtension(image.FileName)}", Guid.NewGuid());
        var filePath="wwwroot/files/"+fileName;
        var fileImagePath = "wwwroot/images/"+imageName;

        using (var stream = new FileStream(fileImagePath, FileMode.Create)) await image.CopyToAsync(stream);
        using (var stream=new FileStream(filePath, FileMode.Create)) await file.CopyToAsync(stream);

        var model = new CompositionModel()
        {
            Name = name,
            ImagePath = "images/" + imageName,
            Path = "files/" + fileName,
            Description = description,
            Genre = genreService.GetById(genreId),
            Author = authorService.GetById(authorId),
            CreateDate = DateOnly.FromDateTime(DateTime.Now)
        };
        compositionService.Create(model);

        return RedirectToAction("List");
    }

    [HttpGet("Edit/{id}")]
    [Authorize]
    public async Task<IActionResult> Edit([FromRoute] int id)
    {
        var model = compositionService.GetById(id);
        var user = await userManager.GetUserAsync(User);
        var userRoles = await userManager.GetRolesAsync(user);

        if (model.Author.Email != user.Email && !userRoles.Contains("admin")) return RedirectToAction("List");
        var viewModel = new CreateCompositionViewModel()
        {
            Composition = model,
            Genres =genreService.GetAll().ToArray()
        };
        return View("Edit", viewModel);
    }

    [HttpPost("Edit")]
    [Authorize]
    public async Task<IActionResult> Edit([FromForm(Name ="id")] int id,[FromForm(Name = "name")] string name, [FromForm(Name = "description")] string description, [FromForm(Name = "genreId")] int genreId)
    {
        var user = await userManager.GetUserAsync(User);
        var userRoles = await userManager.GetRolesAsync(user);

        var model = compositionService.GetById(id);
        if (model.Author.Email != user.Email && !userRoles.Contains("admin")) return RedirectToAction("List");

        model.Name = name;
        model.Description = description;
        model.Genre = genreService.GetById(genreId);

        compositionService.Update(model);
        return RedirectToAction("List");
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult Delete([FromRoute] int id)
    {
        commentService.DeleteCommentsFromComposition(id);
        compositionService.Delete(id);
        return RedirectToAction("List");
    }
}

