using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedTime { get; set; }
        public decimal Sum { get; set; }
        public bool IsFinally { get; set; }
    }
}
