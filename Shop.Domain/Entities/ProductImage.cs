using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Domain.Entities
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ImagePath { get; set; }
    }
}
