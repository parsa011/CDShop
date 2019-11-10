using Shop.Domain.Core;
using Shop.Domain.Core.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Domain.Entities
{
    public class User : BaseEntity<string>
    {
        public User()
        {
            Id = GuidGenerator.NewGuid(5);
            IsDeleted = false;
        }

        [StringLength(20)]
        [Required
        public string Name { get; set; }

        [StringLength(11)]
        [Required]
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }

        [StringLength(6)]
        [Required]
        public string ActivationCode { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
