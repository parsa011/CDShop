using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Common.ViewModels
{
    public class ProductListViewModel
    {
        public string Id { get; set; }

        [StringLength(20)]
        [Required]
        public string Title { get; set; }

        public string ImagePath { get; set; }

        [StringLength(50)]
        [Required]
        public string Summary { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public Category Category { get; set; }
    }

    public class ProductCreateViewModel
    {
        public string Id { get; set; }

        [StringLength(20)]
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        public string Title { get; set; }

        [StringLength(50)]
        [Display(Name = "توضیح کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        [Display(Name = "محتوا")]
        public string Content { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        public decimal Price { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        public int DiskCount { get; set; }

        [Display(Name = "دارای جعبه")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        public bool HasBox { get; set; }

        [Display(Name = "تعداد موجود")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        public int Quantity { get; set; }

        [Display(Name = "اموزش یا راهنما")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        public bool HasHelp { get; set; }

        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        public int CategoryId { get; set; }

        [Display(Name = "عکس های محصول")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        public List<IFormFile> Images { get; set; }
    }

    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
        public List<ProductImage> Images { get; set; }
    }
}
