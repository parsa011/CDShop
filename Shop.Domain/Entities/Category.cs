using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Domain.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        [Required]
        public string Title { get; set; }
        public int ParentId { get; set; }
    }
}
