using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Domain.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
    }
}
