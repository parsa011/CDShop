using Shop.Domain.Core;
using Shop.Domain.Core.BaseEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Entities
{
    public class Address : BaseEntity<string>
    {
        public Address()
        {
            Id = GuidGenerator.NewGuid();
            IsDeleted = false;
        }

        public string Title { get; set; }
        public string FullAddress { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string PostAddress { get; set; }

        public string UserId { get; set; }
    }
}
