using System.Collections.Generic;
using GroceryShop.Models;

public interface IArticlesContext
{
    IEnumerable<Article> GetAll();
    Article GetById(Guid id);
    void Add(Article article);
    void Update(Article article);
    void Delete(Guid id);
}
