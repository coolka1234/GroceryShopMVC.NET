using System.Collections.Generic;
using GroceryShop.Models;
using Microsoft.EntityFrameworkCore;

public class ArticlesDictionaryContext : DbContext,IArticlesContext
{
    public DbSet<Article> Article { get; set; }
    public ArticlesDictionaryContext(DbContextOptions<ArticlesDictionaryContext> options) : base(options) { }
    private readonly Dictionary<int, Article> _articles = new();

    public IEnumerable<Article> GetAll() => _articles.Values;

    public Article GetById(int id) => _articles.ContainsKey(id) ? _articles[id] : null;

    public void Add(Article article) => _articles[article.Id] = article;

    public void Update(Article article)
    {
        if (_articles.ContainsKey(article.Id))
        {
            _articles[article.Id] = article;
        }
    }

    public void Delete(int id) => _articles.Remove(id);
}
