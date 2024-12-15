using System.Linq;
using GroceryShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;

public class ArticlesListContext : DbContext, IArticlesContext
{
    public DbSet<Article> Article { get; set; }
    public void onModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>().HasData(
            new Article
            {
                Id = 1,
                Name = "Milk",
                Price = 1.2,
                bestBefore = DateTime.Now.AddDays(7),
                Category = Category.Dairy
            },
            new Article
            {
                Id = 2,
                Name = "Bread",
                Price = 0.8,
                bestBefore = DateTime.Now.AddDays(2),
                Category = Category.Sweets
            },
            new Article
            {
                Id = 3,
                Name = "Apple",
                Price = 0.5,
                bestBefore = DateTime.Now.AddDays(5),
                Category = Category.Fruits
            },
            new Article
            {
                Id = 4,
                Name = "Carrot",
                Price = 0.3,
                bestBefore = DateTime.Now.AddDays(10),
                Category = Category.Vegetables
            }
        );
        base.OnModelCreating(modelBuilder);
    }

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
