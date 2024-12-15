using System.Collections.Generic;
using GroceryShop.Models;

public class ArticlesDictionaryContext : IArticlesContext
{
    private readonly Dictionary<Guid, Article> _articles = new();

    public IEnumerable<Article> GetAll() => _articles.Values;

    public Article GetById(Guid id) => _articles.ContainsKey(id) ? _articles[id] : null;

    public void Add(Article article) => _articles[article.Id] = article;

    public void Update(Article article)
    {
        if (_articles.ContainsKey(article.Id))
        {
            _articles[article.Id] = article;
        }
    }

    public void Delete(Guid id) => _articles.Remove(id);
}
