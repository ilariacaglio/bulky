using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBook.Models1
{
	public class Product
	{
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Required]
        public string ISBN { get; set; } = null!;
        [Required]
        public string Author { get; set; } = null!;
        [Required]
        [Range(1, 10000)]
        public double ListPrice { get; set; }
        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }
        [Required]
        [Range(1, 10000)]
        public double Price50 { get; set; }
        [Required]
        [Range(1, 10000)]
        public double Price100 { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;
        [Required]
        public int CoverTypeId { get; set; }
        public CoverType CoverType { get; set; } = null!;
    }
}