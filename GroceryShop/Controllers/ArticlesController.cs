using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using GroceryShop.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace GroceryShop.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly MyDbContext _context;
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public ArticlesController(MyDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            ViewBag.Categories = _context.Article.ToList();
            return View(await _context.Article.ToListAsync());
        }

        // GET: Articles/Details/5
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
            ViewBag.Categories = "Category";
            
            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Article.ToList();
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,PriceAsString,ImagePath,CategoryId")] Article article, IFormFile imageFile)
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
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _context.Article.ToList();
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,PriceAsString,ImagePath,CategoryId")] Article article)
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

                    _context.UpdateArticle(_context, article);
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

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
            ViewBag.Categories = "Category";
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Obsolete]
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
    }
}