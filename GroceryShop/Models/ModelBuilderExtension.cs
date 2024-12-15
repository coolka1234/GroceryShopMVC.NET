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
                    Name = "Cheese",
                    Price = 3F,
                    PriceAsString = "3.0",
                    CategoryId = 2,
                    ImagePath = "image/cheese.jpg",
                    Category = Category.Dairy
                }, new Article()
                {
                    Id = 2,
                    Name = "Plum",
                    Price = 2.7F,
                    PriceAsString = "2.7",
                    CategoryId = 4,
                    ImagePath = "image/plum.jpg",
                    Category = Category.Fruits
                }, new Article()
                {
                    Id = 3,
                    Name = "Potato",
                    Price = 4.5F,
                    PriceAsString = "4.5",
                    CategoryId = 2,
                    ImagePath = "image/potato.jpg",
                    Category = Category.Vegetables
                }
            };
           
            modelBuilder.Entity<Article>().HasData(articles);

            Console.WriteLine("Data added to modelBuilder.");
        }
    }

}