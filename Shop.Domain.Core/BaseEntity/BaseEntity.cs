using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Domain.Core.BaseEntity
{
    public class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
