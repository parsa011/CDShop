using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Common.ViewModels
{
    public class ProductListViewModel
    {
        public string Id { get; set; }

        [StringLength(20)]
        [Required]
        public string Title { get; set; }

        [StringLength(50)]
        [Required]
        public string Summary { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public Category Category { get; set; }
    }

    public class ProductCreateViewModel
    {
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
    }
}
