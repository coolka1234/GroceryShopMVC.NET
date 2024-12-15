using System.Linq;
using GroceryShop.Models;

public class ArticlesListContext : IArticlesContext
{
    private readonly List<Article> _articles = new();

    public IEnumerable<Article> GetAll() => _articles;

    public Article GetById(Guid id) => _articles.FirstOrDefault(a => a.Id == id);

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

    public void Delete(Guid id) => _articles.RemoveAll(a => a.Id == id);
}
