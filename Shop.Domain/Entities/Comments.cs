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
            Id = GuidGenerator.NewGuid(5);
            IsDeleted = false;
        }

        public string Title { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public int ProductId { get; set; }
    }
}
