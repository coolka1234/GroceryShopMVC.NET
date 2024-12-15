using System.Linq;
using GroceryShop.Models;
using Microsoft.EntityFrameworkCore;

public class ArticlesListContext : DbContext, IArticlesContext
{
    public DbSet<Article> Article { get; set; }

    public ArticlesListContext(DbContextOptions<ArticlesListContext> options) : base(options) { }

    private readonly List<Article> _articles = new();

    public IEnumerable<Article> GetAll() => _articles;

    public Article GetById(int id) => _articles.FirstOrDefault(a => a.Id == id);

    public void Add(Article article) => _articles.Add(article);

    public void Update(Article article)
    {
        var existing = GetById(article.Id);
        if (existing != null)
        {
            existing.Name = article.Name;
            existing.Price = article.Price;
            existing.bestBefore = article.bestBefore;
            existing.Category = article.Category;
        }
    }

    public void Delete(int id) => _articles.RemoveAll(a => a.Id == id);
}
