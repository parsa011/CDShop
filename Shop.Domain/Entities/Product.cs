using Shop.Domain.Core;
using Shop.Domain.Core.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Domain.Entities
{
    public class Product : BaseEntity<string>
    {
        public Product()
        {
            Id = GuidGenerator.NewGuid();
            IsDeleted = false;
        }

        [StringLength(20)]
        [Required]
        public string Title { get; set; }

        [StringLength(50)]
        [Required]
        public string Summary { get; set; }

        [Required]
        public string Content { get; set; }
        public decimal Price { get; set; }
        public int DiskCount { get; set; }
        public bool HasBox { get; set; }
        public int Quantity { get; set; }
        public bool HasHelp { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
