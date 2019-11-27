using System.ComponentModel.DataAnnotations;

namespace Shop.Common.ViewModels.AddressViewModel
{
    public class AddressCreateViewModel
    {
        public string Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        [MaxLength(20,ErrorMessage = "{0} نمیتواند بیش از 20 کلمه باشد")]
        public string Title { get; set; }

        [Display(Name = "ادرس کامل")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        public string FullAddress { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        [MaxLength(25, ErrorMessage = "{0} نمیتواند بیش از 20 کلمه باشد")]

        public string City { get; set; }

        [Display(Name = " شماره تماس")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "ادرس پستی")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        [DataType(DataType.PostalCode)]
        public string PostAddress { get; set; }
    }
}
