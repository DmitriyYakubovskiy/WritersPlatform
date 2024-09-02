using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WritersPlatform.Models;
using WritersPlatform.Services;

namespace WritersPlatform.Controllers;

[Route("[controller]")]
[ApiController]
public class GenreController : Controller
{
    private readonly IGenreService genreService;

    public GenreController(IGenreService genreService)
    {
        this.genreService = genreService;
    }

    [HttpGet("List")]
    [Authorize]
    public IActionResult List()
    {
        var compositions = genreService.GetAll().ToList();
        return View(compositions.ToArray());
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "admin")]
    public IActionResult GetById([FromRoute] int id)
    {
        var model = genreService.GetById(id);
        if (model == null) return NotFound(id);
        return View("Details", model);
    }

    [HttpGet("Create")]
    [Authorize(Roles = "admin")]
    public IActionResult Create()
    {
        return View("Create", new GenreModel());
    }

    [HttpPost("Create")]
    [Authorize(Roles = "admin")]
    public IActionResult Create([FromForm] GenreModel model)
    {
        genreService.Create(model);
        return RedirectToAction("List");
    }

    [HttpGet("Edit/{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult Edit([FromRoute] int id)
    {
        var model = genreService.GetById(id);
        return View("Edit", model);
    }

    [HttpPost("Edit")]
    [Authorize(Roles = "admin")]
    public IActionResult Edit([FromForm(Name = "Genre")] GenreModel model)
    {
        genreService.Update(model);
        return RedirectToAction("List");
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult Delete([FromRoute] int id)
    {
        genreService.Delete(id);
        return Ok();
    }
}
