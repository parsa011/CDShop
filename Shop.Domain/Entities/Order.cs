using System;
using System.Collections.Generic;
using System.Text;
using Shop.Domain.Core;
using Shop.Domain.Core.BaseEntity;

namespace Shop.Domain.Entities
{
    public class Order : BaseEntity<string>
    {
        public Order()
        {
            Id = GuidGenerator.NewGuid();
            IsDeleted = false;
        }
        public string UserId { get; set; }
        public DateTime CreatedTime { get; set; }
        public decimal Sum { get; set; }
        public bool IsFinally { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
