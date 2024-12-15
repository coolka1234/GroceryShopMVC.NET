using Microsoft.AspNetCore.Mvc;
using GroceryShop.Models;

public class ArticlesController : Controller
{
    private readonly IArticlesContext _context;

    public ArticlesController(IArticlesContext context)
    {
        _context = context;
    }

    public IActionResult Index() => View(_context.GetAll());

    public IActionResult Details(Guid id)
    {
        var article = _context.GetById(id);
        if (article == null) return NotFound();
        return View(article);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(Article article)
    {
        if (ModelState.IsValid)
        {
            _context.Add(article);
            return RedirectToAction(nameof(Index));
        }
        return View(article);
    }

    public IActionResult Edit(Guid id)
    {
        var article = _context.GetById(id);
        if (article == null) return NotFound();
        return View(article);
    }

    [HttpPost]
    public IActionResult Edit(Article article)
    {
        if (ModelState.IsValid)
        {
            _context.Update(article);
            return RedirectToAction(nameof(Index));
        }
        return View(article);
    }

    public IActionResult Delete(Guid id)
    {
        var article = _context.GetById(id);
        if (article == null) return NotFound();
        return View(article);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(Guid id)
    {
        _context.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
