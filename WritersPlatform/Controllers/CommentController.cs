using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WritersPlatform.Models;
using WritersPlatform.Services;

namespace WritersPlatform.Controllers;

[Route("[controller]")]
[ApiController]
public class CommentController : Controller
{
    private readonly ICommentService commentService;
    private readonly IAuthorService authorService;
    private readonly ICompositionService compositionService;

    public CommentController(ICommentService commentService, IAuthorService authorService, ICompositionService compositionService)
    {
        this.commentService = commentService;
        this.authorService = authorService;
        this.compositionService = compositionService;
    }

    [HttpGet("Create")]
    [Authorize]
    public IActionResult Create()
    {
        return RedirectToAction("List", "Composition");
    }

    [HttpPost("Create")]
    [Authorize]
    public IActionResult Create([FromForm(Name = "text")] string text, [FromForm(Name = "authorId")] int authorId, [FromForm(Name = "compositionId")] int compositionId, [FromForm(Name = "rating")] int rating)
    {
        CommentModel model = new CommentModel()
        {
            Author = authorService.GetById(authorId),
            Composition = compositionService.GetById(compositionId),
            Text = text,
            Rating=rating
        };
        commentService.Create(model);
        return RedirectToAction($"{compositionId}", "Composition");
    }

    [HttpDelete("Delete/{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult Delete([FromRoute] int id)
    {
        commentService.Delete(id);
        return Ok();
    }
}
