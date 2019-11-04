using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Core.BaseEntity
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
