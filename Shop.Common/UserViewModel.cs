using System.ComponentModel.DataAnnotations;

namespace Shop.Common.UserViewModel
{
    public class UsersListViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [StringLength(11)]
        [Required]
        public string PhoneNumber { get; set; }

        [StringLength(6)]
        [Required]
        public string ActivationCode { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public string Role { get; set; }
    }

    public class UserEditViewModel
    {     
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        public string Name { get; set; }

        [Display(Name = "شماره تلفن")]
        [StringLength(11)]
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        [Phone(ErrorMessage = "شماره موبایل صحیح نمیباشد")]
        public string PhoneNumber { get; set; }

        [StringLength(6)]
        [Display(Name = "کد فعالسازی")]
        [Required(ErrorMessage = "لطفا {0} را پر کنید")]
        public string ActivationCode { get; set; }


        public bool IsActive { get; set; }

        public string PassWord { get; set; }

        public string Id { get; set; }

        public bool IsAdmin { get; set; }

    }
}
