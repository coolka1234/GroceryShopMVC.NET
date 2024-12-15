using Microsoft.AspNetCore.Mvc;
using GroceryShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
public class ArticlesController : Controller
{
    private readonly ArticlesListContext _context;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public ArticlesController(ArticlesListContext context, IWebHostEnvironment hostingEnvironment)
    {
        _context = context;
        _hostingEnvironment = hostingEnvironment;
    }

    public async Task<IActionResult> Index(){
        return View(await _context.Article.ToListAsync());
    } 

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var article = await _context.Article
            .FirstOrDefaultAsync(m => m.Id == id);
        if (article == null)
        {
            return NotFound();
        }
        return View(article);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,PriceAsString,bestBefore,Price,CategoryId,ImagePath,Category")] Article article, IFormFile imageFile)
    {
        if (ModelState.IsValid)
        {
            float NewPrice;
            CultureInfo cultureInfo = CultureInfo.InvariantCulture;
            float.TryParse(article.PriceAsString, NumberStyles.Float, cultureInfo, out NewPrice);
            article.Price = NewPrice;

            if (imageFile != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "upload");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                article.ImagePath = "upload/"+uniqueFileName;
            } 
            _context.Article.Add(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(article);
    }

    public IActionResult Create() => View();

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null){
             return NotFound();
        }
        var article = await _context.Article.FindAsync(id);
        if (article == null) return NotFound();
        return View(article);
    }

    // [HttpPost]
    // public IActionResult Edit(Article article)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         _context.Update(article);
    //         return RedirectToAction(nameof(Index));
    //     }
    //     return View(article);
    // }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PriceAsString,bestBefore,Price,CategoryId,ImagePath,Category")] Article article)
    {
        if (id != article.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {

                float NewPrice;
                CultureInfo cultureInfo = CultureInfo.InvariantCulture;
                float.TryParse(article.PriceAsString, NumberStyles.Float, cultureInfo, out NewPrice);
                article.Price = NewPrice;

                _context.Article.Update(article);
                System.Console.WriteLine("success");
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(article.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(article);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var article =await _context.Article.FirstOrDefaultAsync(m => m.Id == id);
        if (article == null) return NotFound();
        return View(article);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var article = await _context.Article.FindAsync(id);
        if (article.ImagePath != null)
        {
            var fullPath = Path.Combine(_hostingEnvironment.WebRootPath, article.ImagePath);
            if (System.IO.File.Exists(fullPath) && article.ImagePath != "image/noimage.jpg")
            {
                System.IO.File.Delete(fullPath);
            }
        }
        
        _context.Article.Remove(article);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    private bool ArticleExists(int id)
    {
        return _context.Article.Any(e => e.Id == id);
    }

    // [HttpPost, ActionName("Delete")]
    // public IActionResult DeleteConfirmed(Guid id)
    // {
    //     _context.Delete(id);
    //     return RedirectToAction(nameof(Index));
    // }
}
