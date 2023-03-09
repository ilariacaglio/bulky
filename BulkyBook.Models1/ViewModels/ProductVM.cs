using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Models1.ViewModels
{
	public class ProductVM
	{
        public Product Product { get; set; } = null!;
        public IEnumerable<SelectListItem> CategoryList { get; set; } = null!;
        public IEnumerable<SelectListItem> CoverTypeList { get; set; } = null!;
    }
}