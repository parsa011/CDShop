using Shop.Domain.Core;
using Shop.Domain.Core.BaseEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Entities
{
    public class Comment : BaseEntity<string>
    {
        public Comment()
        {
            Id = GuidGenerator.NewGuid();
            IsDeleted = false;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public string ProductId { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
