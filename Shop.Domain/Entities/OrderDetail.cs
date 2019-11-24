using Shop.Domain.Core;
using Shop.Domain.Core.BaseEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Entities
{
    public class OrderDetail : BaseEntity<string>
    {
        public OrderDetail()
        {
            Id = GuidGenerator.NewGuid();
            IsDeleted = false;
        }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }

        public Order Order { get; set; }
    }
}
