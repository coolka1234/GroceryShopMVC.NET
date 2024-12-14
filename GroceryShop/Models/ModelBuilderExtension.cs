using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using System.Collections.Generic;

namespace GroceryShop.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            

            var articles = new List<Article>
            {
                 new Article()
                {
                    Id = 1,
                    Name = "Apple",
                    Price = 2.5F,
                    PriceAsString = "2.5",
                    CategoryId = 2,
                    ImagePath = "image/apple.jpg",
                    Category = Category.Fruits
                }, new Article()
                {
                    Id = 2,
                    Name = "Carrot",
                    Price = 3.0F,
                    PriceAsString = "3.0",
                    CategoryId = 4,
                    ImagePath = "image/carrot.png",
                    Category = Category.Vegetables
                }, new Article()
                {
                    Id = 3,
                    Name = "Milk",
                    Price = 4.5F,
                    PriceAsString = "4.5",
                    CategoryId = 2,
                    ImagePath = "image/milk.jpg",
                    Category = Category.Dairy
                }
            };
           

            modelBuilder.Entity<Article>().HasData(articles);

            Console.WriteLine("Data added to modelBuilder.");
        }
    }

}