using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Globalization;
using System.Linq;

namespace GroceryShop.Models
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) 
        { 

        }

        public DbSet<Article> Article { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("on model creating executing");
            modelBuilder.Seed();        
            // modelBuilder.Entity<Article>()
            // .Property(a => a.Category)
            // .HasConversion<string>();
            base.OnModelCreating(modelBuilder);
        }

        public void UpdateArticle(DbContext context, Article article)
        {
            var existingArticle = context.Set<Article>().FirstOrDefault(a => a.Id == article.Id);

            if (existingArticle != null)
            {
                existingArticle.Name = article.Name;
                existingArticle.PriceAsString = article.PriceAsString;
                existingArticle.CategoryId = article.CategoryId;

                existingArticle.Price = article.Price;
            }
        }
        private String[] categories() {
            return Enum.GetNames(typeof(Category));
        }
    }
}