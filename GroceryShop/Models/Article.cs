using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryShop.Models
{
    public enum Category
    {
        Sweets,
        Dairy,
        Vegetables,
        Fruits,
    }
    public class Article
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        public string Name { get; set; }
        public string PriceAsString { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime bestBefore { get; set; }
        [DataType(DataType.Currency)]
        [Required]
        public float Price { get; set; }
        public int CategoryId { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [Display(Name = "Image")]
        public string ImagePath { get; set; }
        public Category Category { get; set; }
    }
}